using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OcularInk.Characters.Protagonist
{
    public class BrushController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem brushParticle;
        [SerializeField] private float brushRate;
        [SerializeField] private TextMeshProUGUI debugText;
        [SerializeField] private GameObject[] shards;
        public ObjectPool brushAreaPool;

        public bool preventBrush;

        private float brushStartTime;

        private Camera _camera;
        public Vector3 Velocity { get; private set; }
        private Vector3 _oldPos;
        private Vector3 _targetPos;
        private bool _isBrushing;
        private float _attackProcTime;

        private float _deltaDistance;
        private float _accumulatedDistance = 0;

        public static float dist;

        public int UITouch { get; private set; } = -1;
        public bool mouse;
        void Start()
        {
            _camera = Camera.main;
            _attackProcTime = -(1 / brushRate);

            var unlockedPowers = GameManager.GameData.UnlockedSuperpowers;

            for (int i = 0; i < shards.Length; i++)
            {
                shards[i].SetActive(unlockedPowers[i]);
            }
        }

        void Update()
        {
            if (GameManager.State != GameState.Playing)
                return;

            CheckTouch();
            MoveBrush();
            Attack();
            CalculateVelocity();
        }

        private void CheckTouch()
        {
            if (Input.touches.Length == 0)
            {
                UITouch = -1;
                return;
            }

            foreach (var touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    UITouch = id;
                }
            }
        }

        private void MoveBrush()
        {
            if (mouse)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _accumulatedDistance = 0f;
                    brushStartTime = Time.time;
                }
            }
            else
            {
                if (Input.touches.Length > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        brushStartTime = Time.time;
                    }
                }
            }



            if (mouse)
            {

                var mousePos = Input.mousePosition;
                var ray = _camera.ScreenPointToRay(mousePos);
                var layerMask = LayerMask.GetMask("Terrain");
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, layerMask))
                {
                    var pos = hit.point;

                    if (!EventSystem.current.IsPointerOverGameObject())
                        _targetPos = new Vector3(pos.x, hit.point.y + 2.5f, pos.z+13.25f);
                    print(hit.point.y + "yyyyy"+pos.x+"xxxxxx"+pos.z+"zzzz");
                    AddToDistance();
                }
            }
            else
            {

                var touches = Input.touches.Where(t => t.fingerId != UITouch).ToList();
                if (touches.Count == 0)
                {
                    _targetPos = transform.position;
                }
                else
                {

                    var ray = _camera.ScreenPointToRay(touches[0].position);
                    var layerMask = LayerMask.GetMask("Terrain");
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100, layerMask))
                    {
                        var pos = hit.point;

                        if (!EventSystem.current.IsPointerOverGameObject(touches[0].fingerId))
                        {
                            _targetPos = new Vector3(pos.x, hit.point.y + 2.5f, pos.z+13.25f);
                        }
                    }
                    AddToDistance();
                }


            }
                _targetPos.y = Mathf.Clamp(_targetPos.y, 22f, 33f);

            transform.position = _targetPos;
        }

        private void CalculateVelocity()
        {
#if UNITY_ANDROID || UNITY_IOS

            if (Input.touches.Length == 0)
                return;

            var touchPos = Input.GetTouch(0).position;

            Velocity = (touchPos - (Vector2)_oldPos) / Time.deltaTime;

            _oldPos = touchPos;

#else
            Velocity = (Input.mousePosition - _oldPos) / Time.deltaTime;
            
            _oldPos = Input.mousePosition;

#endif
        }

        private void AddToDistance()
        {
            _accumulatedDistance += _deltaDistance;
        }

        private List<BrushArea> current = new List<BrushArea>();

        private void Attack()
        {
#if true
            if (Input.touches.Length > 0)
            {
                if (Input.touches.Where(t => t.fingerId != UITouch).ToList().Count == 0)
                {
                    HandleRelease();
                    return;
                }
            }
            if (Input.touches.Length == 0)
            {
                HandleRelease();
                return;
            }
#else

            if (!Input.GetMouseButton(0))
            {
                HandleRelease();
                return;
            }
#endif
            animator.SetBool("Attacking", true);
            _isBrushing = true;

            // Proc
            ProcBrush();
            _accumulatedDistance = 0f;
        }

        public void CenterBrush()
        {
            var ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
            var layerMask = LayerMask.GetMask("Terrain");
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                var pos = hit.point;

                if (!EventSystem.current.IsPointerOverGameObject())
                    _targetPos = new Vector3(pos.x, hit.point.y + 2.5f, pos.z);

                transform.position = pos;
            }

        }

        private void HandleRelease()
        {
            _isBrushing = false;
            animator.SetBool("Attacking", false);

            var emission = brushParticle.emission;
            emission.rateOverDistance = 0f;

            if (current.Count > 0)
            {
                current[0].transform.LookAt(current[^1].transform);

                foreach (var brushArea in current)
                {
                    brushArea.Activate();
                    brushArea.transform.eulerAngles = current[0].transform.eulerAngles;
                }

                dist = Vector3.Distance(current[^1].transform.position, current[0].transform.position);

                current.Clear();
            }
        }

        private void ProcBrush()
        {
#if !UNITY_EDITOR
            if (Input.touches.Length == 0 || Input.touches.Length > 2)
                return;
#endif

            if (Time.timeScale < 0.1f)
            {
                return;
            }

            if (Time.time - brushStartTime < Time.deltaTime)
                return;

            _attackProcTime = Time.time;

            var pos = transform.position;
            pos.y = transform.position.y - 2.3f;

            var brushArea = brushAreaPool.GetObject();

            if (brushArea == null)
                return;

            brushArea.transform.forward = transform.forward;
            brushArea.transform.position = pos;


            if (!preventBrush && Velocity.magnitude > 0.1f)
            {
                brushParticle.Emit(1);
            }

            current.Add(brushArea);
        }
    }
}
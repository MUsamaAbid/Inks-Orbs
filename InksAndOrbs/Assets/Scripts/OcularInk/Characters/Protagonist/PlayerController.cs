using System;
using DG.Tweening;
using UnityEngine;

namespace OcularInk.Characters.Protagonist
{
    public class PlayerController : MonoBehaviour
    {
        public BrushController brushController;
        [SerializeField] private CharacterController characterCtrl;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float baseHealth = 100f;
        [SerializeField] private GameObject deathFx;

        [SerializeField] private GameObject speedParticle;
        [SerializeField] private GameObject shieldParticle;
        [SerializeField] private GameObject immunityParticle;

        public ShieldPower shieldPower;
        public ImmunityPower immunityPower;
        public SpeedPower speedPower;

        private bool shieldBonusActive;
        private bool isImmune;
        public bool preventAttacks;

        private float health;
        private float inputX;
        private float inputY;

        private float lastMoveTime;
        public static bool ForceDisable;

        // Start is called before the first frame update
        void Awake()
        {
            baseHealth += GameManager.GameData.HealthLevel * 25f;
            health = baseHealth;
            GameManager.Instance.PlayerController = this;
        }

        private float rotx, roty;

        private void Update()
        {
            if (ForceDisable)
                return;
        
            AttractItems();
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            if (health < 0f)
                return;
            
            if (transform.position.y < 0.58f)
            {
                transform.position = new Vector3(transform.position.x, 0.59f, transform.position.z);
                return;
            }
            
            inputX = SimpleInput.GetAxis("Horizontal") * speed * Time.deltaTime;
            inputY = SimpleInput.GetAxis("Vertical") * speed * Time.deltaTime;
            
            var moveVector = new Vector3(inputX, 0, inputY);

            if (moveVector.magnitude > 0)
                lastMoveTime = Time.time;

            if (!characterCtrl.isGrounded)
            {
                moveVector += Physics.gravity * Time.deltaTime;
            }

            characterCtrl.Move(moveVector);

            moveVector.y = 0;
        }

        private void RotatePlayer()
        {
            if (Time.time - lastMoveTime > 0.25f)
            {
                var lookDir = brushController.transform.position;
                lookDir.y = transform.position.y;

                Quaternion targetRot = Quaternion.LookRotation(lookDir - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 4f);
            }
            
            rotx = inputY * 55f;
            roty = -inputX * 55f;

            transform.Rotate(new Vector3(rotx, 0, roty), Space.World);
        }
        
        private void AttractItems()
        {
            var layerMask = LayerMask.GetMask("Pickup");

            var overlap = Physics.OverlapSphere(transform.position, 12f, layerMask);

            foreach (var obj in overlap)
            {
                obj.transform.position = Vector3.Lerp(obj.transform.position, transform.position, Time.deltaTime * 10f);
                obj.isTrigger = true;
            }
        }

        public void TakeDamage(float damage)
        {

            if (health < 0f)
                return;

            if (isImmune)
                return;

            if (shieldBonusActive)
                return;

            if (preventAttacks)
                return;
            
            health -= damage;

            health = Mathf.Clamp(health, -10f, baseHealth);
            

            if (health < 0f)
            {
                // game over
                Die();
            }

            GameManager.Instance.GameController.SetHealth(health / baseHealth);


            if (damage > 0)
            {
                GameManager.Instance.GameController.HitFeedback();
                AudioManager.Instance.PlayAudio("PlayerHurt");
            }
            
        }

        private void Die()
        {
            Instantiate(deathFx, transform.position, Quaternion.identity);
            AudioManager.Instance.PlayAudio("GameOver");
            AudioManager.Instance.ToggleMusic(false);
            
            GameManager.Instance.GameController.GameOver();
        }

        public void AddHealth(float value)
        {
            health += value;
            
            GameManager.Instance.GameController.SetHealth(health / baseHealth);
        }
        public void ToggleSpeedBonus(bool value)
        {
            if (value)
            {
                speed = 16f;
                speedParticle.SetActive(true);
                UIManager.Instance.GetCanvas<GameCanvas>().superpowerPanel.ShowTimer(0, 10f);
            }
            else
            {
                speed = 8f;
                speedParticle.SetActive(false);
            }
        }

        public void ToggleShield(bool value)
        {
            if (value)
                UIManager.Instance.GetCanvas<GameCanvas>().superpowerPanel.ShowTimer(1, 10f);

            shieldParticle.SetActive(value);
            shieldBonusActive = value;
        }

        public void ToggleImmunity(bool value)
        {
            if (value)
                UIManager.Instance.GetCanvas<GameCanvas>().superpowerPanel.ShowTimer(2, 10f);

            immunityParticle.SetActive(value);
            isImmune = value;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Health"))
            {
                
                AudioManager.Instance.PlayAudio("HealthPickup");
                AddHealth(5f);
            
                collision.collider.transform.DOScale(0f, 0.5f).onComplete = () =>
                    Destroy(collision.collider.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ConfineArea"))
            {
                GameManager.Instance.GameController.UpdateConfineArea(other);
            }
            else if (other.CompareTag("Finish"))
            {
                GameManager.Instance.GameController.FinishGame();
            }
        }
    }
}
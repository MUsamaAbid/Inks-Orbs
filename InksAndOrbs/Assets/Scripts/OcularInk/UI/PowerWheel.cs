using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using OcularInk.Characters.Protagonist;
using TMPro;
using UnityEngine;

public class PowerWheel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skillAreaRenderer;
    [SerializeField] private Transform wheel;

    [SerializeField] private WheelSkill[] skills;

    [SerializeField] private TextMeshPro skillTitle;
    [SerializeField] private TextMeshPro skillDesc;

    [SerializeField] private TextMeshProUGUI debugText;

    private bool isMouseDown = false;
    private float mouseDownTime = 0f;

    private WheelSkill selectedSkill;

    private BrushController brushController;

    private bool isActive;
    private bool canActivateSkill;

    private void Start()
    {
        var unlockedPowers = GameManager.GameData.UnlockedSuperpowers;
        brushController = GameManager.Instance.PlayerController.brushController;
        isActive = unlockedPowers.Any(superpower => superpower);
        
        for (var i = 0; i < skills.Length; i++)
        {
            var skill = skills[i];
            skill.gameObject.SetActive(unlockedPowers[i]);
        }
    }

    private void Deactivate()
    {
        if (!canActivateSkill)
            return;
        
        skillAreaRenderer.DOFade(0f, 0.3f);
        skillAreaRenderer.transform.DOScale(0f, 0.3f).SetEase(Ease.InBack);
        Time.timeScale = 1f;
        isMouseDown = false;
        brushController.preventBrush = false;
        canActivateSkill = false;
    }

    private void Update()
    {
        if (!isActive)
            return;
        
        if (brushController.Velocity.magnitude >= 1f)
        {
            isMouseDown = false;
            return;
        }
        
        ScanClicks();
        ScanSkillSelection();
    }

    private void ScanClicks()
    {
        float holdTime = 0f;
#if UNITY_ANDROID || UNITY_IOS
        
        if (Input.touches.Length == 0 && canActivateSkill)
        {
            if (selectedSkill != null)
            {
                CastSkill();
            }
            Deactivate();

            return;
        }

        if (Input.touches.Length == 0)
            return;
        
        var touch = Input.GetTouch(0);

        if (!isMouseDown && touch.phase is TouchPhase.Began or TouchPhase.Stationary)
        {
            isMouseDown = true;
            mouseDownTime = Time.time;
        }

        var text = $"TP: {touch.phase} " +
                         $"MD: {mouseDownTime} " +
                         $"IS MD: {isMouseDown} " +
                         $"HT: {holdTime}";
        
        UIManager.Instance.GetCanvas<GameCanvas>().SetDebugText(text);
        
        holdTime = Time.time - mouseDownTime;

        if (touch.phase is TouchPhase.Stationary && holdTime >= 0.5f && isMouseDown && brushController.UITouch != touch.fingerId)
        {
            isMouseDown = false;
            brushController.preventBrush = true;
            PlaceMarker();
        }

        if (touch.phase is TouchPhase.Canceled or TouchPhase.Ended)
        {
            if (selectedSkill != null)
            {
                CastSkill();
            }
            Deactivate();
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            mouseDownTime = Time.time;
        }
#endif
    }

    private void ScanSkillSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var brushPos = GameManager.Instance.PlayerController.brushController.transform.position;
        brushPos.y = 0f;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var ordered = skills
                .Where(skill => skill.gameObject.activeSelf)
                .Select(obj => new { Distance = Vector3.Distance(obj.transform.position, brushPos), Skill = obj })
                .OrderBy(obj => obj.Distance)
                .ToArray();

            bool anySelected = false;

            for (var i = 0; i < ordered.Length; i++)
            {
                var wheelSkill = ordered[i];

                bool isSelected = i == 0 && wheelSkill.Distance <= 5f;

                wheelSkill.Skill.Toggle(isSelected);

                if (isSelected)
                {
                    anySelected = true;
                    selectedSkill = wheelSkill.Skill;
                    
                    skillTitle.text = wheelSkill.Skill.SkillData.Name;
                    skillDesc.text = wheelSkill.Skill.SkillData.Description;
                }
            }

            if (!anySelected)
            {
                selectedSkill = null;
                skillTitle.text = string.Empty;
                skillDesc.text = string.Empty;
            }
        }
    }

    private void PlaceMarker()
    {
        skillAreaRenderer.DOFade(1f, 0.3f).From(0f).SetUpdate(true);
        skillAreaRenderer.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetUpdate(true).onComplete =
            () => canActivateSkill = true;
        var pos = brushController.transform.position + Vector3.down;
        pos.y = 1;
        skillAreaRenderer.transform.position = pos;
        Time.timeScale = 0f;
    }

    private void CastSkill()
    {
        if (selectedSkill == null)
            return;

        selectedSkill.Activate(skillAreaRenderer.transform.position);
        
        Debug.Log("Selected skill: " + selectedSkill);
        selectedSkill = null;
        
    }
}
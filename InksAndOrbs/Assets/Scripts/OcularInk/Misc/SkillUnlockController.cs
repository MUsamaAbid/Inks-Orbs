using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUnlockController : MonoBehaviour
{
    [SerializeField] private GameObject[] shards;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDesc;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private GameObject shardUnlockFx;

    private SkillData unlockedSkill;

    public static int SkillIndex = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        unlockedSkill = Resources.Load<SkillData>($"Skills/SkillData_{SkillIndex + 1}");
        StartCoroutine(ShardUnlockFlow());
    }

    private IEnumerator ShardUnlockFlow()
    {
        skillName.text = unlockedSkill.Name;
        skillDesc.text = unlockedSkill.Description;

        iconImage.sprite = unlockedSkill.Icon;

        for (int i = 0; i < shards.Length; i++)
        {
            shards[i].SetActive(i <= SkillIndex - 1);
        }

        yield return new WaitForSeconds(2f);
        shards[SkillIndex].SetActive(true);
        shards[SkillIndex].transform.DOScale(shards[SkillIndex].transform.localScale, 0.5f).From(Vector3.zero).SetEase(Ease.OutBack);

        Instantiate(shardUnlockFx, shards[SkillIndex].transform.position, Quaternion.identity);
        AudioManager.Instance.PlayAudio("Sparkle");
        AudioManager.Instance.Vibrate(VibrationType.Medium);
        
        yield return new WaitForSeconds(2f);
        canvasGroup.DOFade(1f, 0.5f);
        
        yield return new WaitForSeconds(3f);
        continueButton.SetActive(true);
        continueButton.transform.DOScale(1f, 0.3f).From(0f).SetEase(Ease.OutBack);

    }

    public void Continue()
    {
        SceneMaster.EnterLevel(GameManager.GameData.CurrentLevel);
    }
}

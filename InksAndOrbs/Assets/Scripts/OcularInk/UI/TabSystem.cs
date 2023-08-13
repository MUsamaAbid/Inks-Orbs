using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    public Button[] tabButtons;
    public TextMeshProUGUI[] tabButtonTexts;
    public CanvasGroup[] tabContents;

    private int currentTabIndex;
    private float fadeDuration = 0.2f;

    public Sprite buttonDefault;
    public Sprite buttonActive;

    private void Start()
    {
        // Add button click listeners
        for (int i = 0; i < tabButtons.Length; i++)
        {
            int index = i; // Create a local copy of the index variable for the listener
            tabButtons[i].onClick.AddListener(() => ShowTab(index));
        }
    }

    private void ShowTab(int index)
    {
        if (currentTabIndex == index)
            return;
        
        // Disable the current tab content with fade out effect
        CanvasGroup currentTabContent = tabContents[currentTabIndex];
        currentTabContent.DOFade(0f, fadeDuration).OnComplete(() => currentTabContent.gameObject.SetActive(false));

        // Enable the selected tab content with fade in effect
        CanvasGroup selectedTabContent = tabContents[index];
        selectedTabContent.alpha = 0f;
        selectedTabContent.gameObject.SetActive(true);
        selectedTabContent.DOFade(1f, fadeDuration);

        // Update the current tab index
        currentTabIndex = index;

        for (int i = 0; i < tabButtons.Length; i++)
        {
            var textMesh = tabButtonTexts[i];

            textMesh.color = i == index ? new Color32(45, 160, 46, 255) : Color.white;

            tabButtons[i].GetComponent<Image>().sprite = i == index ? buttonActive : buttonDefault;
        }
    }
}

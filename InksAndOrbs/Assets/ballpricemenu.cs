using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballpricemenu : MonoBehaviour
{
    public GameObject[] purchasebtns;
    public GameObject[] selectedbuttons;
    public GameObject[] selectbuttons;
    public void OnEnable()
    {
       
        if (PlayerPrefs.GetInt("ball0") == 1)
        {
            purchasebtns[0].SetActive(false);
            selectbuttons[0].SetActive(false);
            selectedbuttons[0].SetActive(false);
        }
        else
        {
            purchasebtns[0].SetActive(true);
            selectbuttons[0].SetActive(false);
            selectedbuttons[0].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball1") == 1)
        {
            purchasebtns[1].SetActive(false);
            selectbuttons[1].SetActive(false);
            selectedbuttons[1].SetActive(false);
        }
        else
        {
            purchasebtns[1].SetActive(true);
            selectbuttons[1].SetActive(false);
            selectedbuttons[1].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball2") == 1)
        {
            purchasebtns[2].SetActive(false);
            selectbuttons[2].SetActive(false);
            selectedbuttons[2].SetActive(false);
        }
        else
        {
            purchasebtns[2].SetActive(true);
            selectbuttons[2].SetActive(false);
            selectedbuttons[2].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball3") == 1)
        {
            purchasebtns[3].SetActive(false);
            selectbuttons[3].SetActive(false);
            selectedbuttons[3].SetActive(false);
        }
        else
        {
            purchasebtns[3].SetActive(true);
            selectbuttons[3].SetActive(false);
            selectedbuttons[3].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball4") == 1)
        {
            purchasebtns[4].SetActive(false);
            selectbuttons[4].SetActive(false);
            selectedbuttons[4].SetActive(false);
        }
        else
        {
            purchasebtns[4].SetActive(true);
            selectbuttons[4].SetActive(false);
            selectedbuttons[4].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball5") == 1)
        {
            purchasebtns[5].SetActive(false);
            selectbuttons[5].SetActive(false);
            selectedbuttons[5].SetActive(false);
        }
        else
        {
            purchasebtns[5].SetActive(true);
            selectbuttons[5].SetActive(false);
            selectedbuttons[5].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball6") == 1)
        {
            purchasebtns[6].SetActive(false);
            selectbuttons[6].SetActive(false);
            selectedbuttons[6].SetActive(false);
        }
        else
        {
            purchasebtns[6].SetActive(true);
            selectbuttons[6].SetActive(false);
            selectedbuttons[6].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball7") == 1)
        {
            purchasebtns[7].SetActive(false);
            selectbuttons[7].SetActive(false);
            selectedbuttons[7].SetActive(false);
        }
        else
        {
            purchasebtns[7].SetActive(true);
            selectbuttons[7].SetActive(false);
            selectedbuttons[7].SetActive(false);
        }
        if (PlayerPrefs.GetInt("ball8") == 1)
        {
            purchasebtns[8].SetActive(false);
            selectbuttons[8].SetActive(false);
            selectedbuttons[8].SetActive(false);
        }
        else
        {
            purchasebtns[8].SetActive(true);
            selectbuttons[8].SetActive(false);
            selectedbuttons[8].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 0)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[0].SetActive(true);
            selectbuttons[0].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 1)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[1].SetActive(true);
            selectbuttons[1].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 2)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[2].SetActive(true);
            selectbuttons[2].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 3)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[3].SetActive(true);
            selectbuttons[3].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 4)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[4].SetActive(true);
            selectbuttons[4].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 5)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[5].SetActive(true);
            selectbuttons[5].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 6)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[6].SetActive(true);
            selectbuttons[6].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 7)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[7].SetActive(true);
            selectbuttons[7].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selectedball") == 8)
        {
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[8].SetActive(true);
            selectbuttons[8].SetActive(false);
        }
    }


    public void purchaseball(int ballno)
    {
        var coins = GameManager.GameData.Money;
        if (coins < 100)
        {
            AlertManager.Instance.ShowSingleAlert("PURCHASE FAILED", "Insufficient balance");
            return;
        }
        GameManager.GameData.Money -= 100;

        AlertManager.Instance.ShowSingleAlert("SUCCESS", "Purchased superpower successfully!");
        PlayerPrefs.SetInt("ball" + ballno, 1);
        purchasebtns[ballno].SetActive(false);
        for (int i = 0; i < selectedbuttons.Length; i++)
        {
            if (PlayerPrefs.GetInt("ball" + i) == 1)
            {
                selectedbuttons[i].SetActive(false);
                selectbuttons[i].SetActive(true);
            }
            else
            {
                selectedbuttons[i].SetActive(false);
                selectbuttons[i].SetActive(false);
            }
        }
        selectedbuttons[ballno].SetActive(true);
        selectbuttons[ballno].SetActive(false);
        PlayerPrefs.SetInt("selectedball", ballno);
        DataManager.Save();

    }
    public void selecball(int ballno)
    {
       
            for (int i = 0; i < selectedbuttons.Length; i++)
            {
                if (PlayerPrefs.GetInt("ball" + i) == 1)
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(true);
                }
                else
                {
                    selectedbuttons[i].SetActive(false);
                    selectbuttons[i].SetActive(false);
                }
            }
            selectedbuttons[ballno].SetActive(true);
            selectbuttons[ballno].SetActive(false);
        }
    
}

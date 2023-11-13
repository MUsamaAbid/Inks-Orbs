using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmaterialscript : MonoBehaviour
{
    public GameObject[] allballs;
    public void Start()
    {
        if (PlayerPrefs.GetInt("selectedball") == 0)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[0].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 1)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[1].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 2)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[2].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 3)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[3].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 4)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[4].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 5)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[5].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 6)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[6].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 7)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[7].SetActive(true);
        }
        if (PlayerPrefs.GetInt("selectedball") == 8)
        {
            for (int i = 0; i < allballs.Length; i++)
            {
                allballs[i].SetActive(false);
            }
            allballs[8].SetActive(true);
        }
    }

}

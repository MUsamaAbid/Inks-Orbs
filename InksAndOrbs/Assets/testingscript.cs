using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingscript : MonoBehaviour
{
    public void testbtn()
    {
        GameManager.Instance.GameController.FinishGame();
    }
}

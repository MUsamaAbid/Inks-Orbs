using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haseebcutscenescript : MonoBehaviour
{
    public float mytimer;
    void Start()
    {
        Invoke("callgamefunc", mytimer);
    }

    public void callgamefunc()
    {
        GameController.instance.destroycutscene();
    }
}

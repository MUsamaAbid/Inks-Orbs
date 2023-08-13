using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPower : Superpower
{
    public override void Activate()
    {
        var playerCtrl = GameManager.Instance.PlayerController;

        if (playerCtrl.shieldPower != null)
        {
            Destroy(playerCtrl.shieldPower);
        }

        playerCtrl.shieldPower = this;

        playerCtrl.ToggleShield(true);
        StartCoroutine(DisablePower());
    }

    private IEnumerator DisablePower()
    {
        yield return new WaitForSeconds(10f);

        Disable();
    }
    

    public override void Disable()
    {
        GameManager.Instance.PlayerController.ToggleShield(false);
    }
}

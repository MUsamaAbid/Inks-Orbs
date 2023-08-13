using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityPower : Superpower
{
    public override void Activate()
    {
        CancelInvoke(nameof(Disable));
        var playerCtrl = GameManager.Instance.PlayerController;

        if (playerCtrl.immunityPower != null)
        {
            Destroy(playerCtrl.immunityPower);
        }

        playerCtrl.immunityPower = this;

        transform.position = playerCtrl.transform.position;
        playerCtrl.ToggleImmunity(true);
        Invoke(nameof(Disable), 10f);
    }

    public override void Disable()
    {
        var player = GameManager.Instance.PlayerController;
        player.ToggleImmunity(false);
    }
}

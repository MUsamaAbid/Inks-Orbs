using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPower : Superpower
{
    public override void Activate()
    {
        CancelInvoke(nameof(Disable));
        var playerCtrl = GameManager.Instance.PlayerController;

        if (playerCtrl.speedPower != null)
        {
            Destroy(playerCtrl.speedPower);
        }

        playerCtrl.speedPower = this;

        transform.position = playerCtrl.transform.position;
        playerCtrl.ToggleSpeedBonus(true);

        Invoke(nameof(Disable), 10f);
    }

    public override void Disable()
    {
        var player = GameManager.Instance.PlayerController;
        player.ToggleSpeedBonus(false);
    }
}

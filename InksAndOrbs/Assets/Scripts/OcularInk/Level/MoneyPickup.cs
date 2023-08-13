using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickup : PickupObject
{
   public override void OnHit(Collider other)
   {
      base.OnHit(other);
      
      AudioManager.Instance.PlayAudio("MoneyPickup");
      GameManager.Instance.GameController.AddMoney(Mathf.RoundToInt(transform.lossyScale.x * 5f));
      
      Destroy(gameObject);
   }
}

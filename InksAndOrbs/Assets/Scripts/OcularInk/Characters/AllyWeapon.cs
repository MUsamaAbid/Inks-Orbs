using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Characters;
using UnityEngine;

public abstract class AllyWeapon : MonoBehaviour
{
    [SerializeField] protected WeaponType weaponType; 
    public abstract void Fire();

    protected enum WeaponType
    {
        Melee, Ranged
    }

    public void SetTarget(Vector3 targetPos)
    {
        var targetRotation = Quaternion.LookRotation(targetPos - transform.position);

        transform.DOKill();
        transform.DORotateQuaternion(targetRotation, 0.2f);
    }
}
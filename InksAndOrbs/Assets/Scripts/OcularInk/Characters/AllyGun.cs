using System.Collections;
using System.Collections.Generic;
using OcularInk.Characters;
using UnityEngine;

public class AllyGun : AllyWeapon
{
    [SerializeField] private WeaponProjectile projectile;
    [SerializeField] private Transform projectileSpawnPos;

    public override void Fire()
    {
        var projectileInstance = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity);
        projectileInstance.Fire(transform.forward);
    }
}

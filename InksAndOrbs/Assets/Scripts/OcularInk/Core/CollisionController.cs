using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private Dictionary<string, float> damageCollidables = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CollisionController = this;
    }

    public void RegisterDamageCollidable(string instanceID, float damage)
    {
        damageCollidables.Add(instanceID, damage);
    }

    public float RetrieveDamage(string instanceID)
    {
        if (damageCollidables.ContainsKey(instanceID))
            return damageCollidables[instanceID];

        return 0;
    }
}
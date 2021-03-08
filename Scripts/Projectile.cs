using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int minRndmDmgBoost;
    public int maxRndmDmgBoost;
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        int boost = UnityEngine.Random.Range(minRndmDmgBoost, maxRndmDmgBoost);
        if (enemy != null) 
        {
            enemy.TakeDamage(WeaponHolder.instance.EquippedWeps[0].Damage + boost);
            Destroy(gameObject);
        }
    }
}

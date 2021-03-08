using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int monsterDamage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            PlayerHealth.instance.TakeDamage(monsterDamage);
        }
        //Debug.Log("Collision");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        LynBeatEmUp player = other.GetComponent<LynBeatEmUp>();
        if (enemy != null)
        {
            enemy.TookDamage(damage);
        }

        if (player != null)
        {
            player.TookDamage(damage);
        }
    }
}

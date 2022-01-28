using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill: MonoBehaviour
{
    private GameObject player;

    //Damage the spell is going to do on hit
    public int spellDamage = 35;

    //Check the collision of the spell with the trigger collider of the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other != null)
        {
            Destroy(gameObject);

            player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<PlayerCombat>().PlayerTakingDamage(spellDamage);
        }
    }
}

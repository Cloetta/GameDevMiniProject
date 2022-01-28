using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Units/Enemy")]


public class Enemy : ScriptableObject
{
    //Storing all the stats of the enemy

    new public string name = "New Enemy";

    public int maxHP = 0;
    public int meleeDamage = 0;
    public int rangedDamage = 0;
    public float attackRate = 0f;

    public bool isDefaultEnemy = false;


 
}

    

    


    






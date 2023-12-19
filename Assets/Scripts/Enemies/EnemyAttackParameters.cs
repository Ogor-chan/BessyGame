using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackParameters : MonoBehaviour
{
    public int AttackDamage = 40;

    public bool IsEnemyMelee = true;
    public float TimeToAttack = .25f;

    private void Start()
    {
        if(!IsEnemyMelee) 
        {
            TimeToAttack = 0f;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent1 : MonoBehaviour
{
    [SerializeField] int health = 1000;

    [SerializeField] string[] EnemyMeleeAttackRange;
    [SerializeField] string[] EnemyRangeAttackItem;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.GetComponent<EnemyAttackParameters>().IsEnemyMelee)
        {
          TakeDamage(collision, EnemyRangeAttackItem);

        }
        else if (collision.GetComponent<EnemyAttackParameters>().IsEnemyMelee)
        {
            StartCoroutine(DelayedMeleeDamage(collision));
        }
    }

    private IEnumerator DelayedMeleeDamage(Collider2D collision)
    {
        yield return new WaitForSeconds(collision.GetComponent<EnemyAttackParameters>().TimeToAttack);
        TakeDamage(collision, EnemyMeleeAttackRange);
    }

    private void TakeDamage(Collider2D collision, string[] tags)
    {
            foreach (var tag in tags)
          {

            if (collision.gameObject.CompareTag(tag))
            {
                Debug.Log("Wykryto damagable item: " + tag);
                health = health - collision.GetComponent<EnemyAttackParameters>().AttackDamage;
                Debug.Log("Gracz otrzymal :" + collision.GetComponent<EnemyAttackParameters>().AttackDamage + " obrazen");

                }

            }
    }




}

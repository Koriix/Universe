using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }

    public int CurrentDamage { get; set; }

    private IEnemy tmpenemy;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    

    public void PeformAttack(int damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("Base_Attack");
        if(tmpenemy != null)
            tmpenemy.TakeDamage(CurrentDamage);
        Debug.Log("Current Damage: " + CurrentDamage);
    }

    public void PeformSpecialAttack()
    {
        animator.SetTrigger("Special_Attack");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            //col.GetComponent<IEnemy>().TakeDamage(CharacterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue());
            tmpenemy = col.GetComponent<IEnemy>();
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Enemy")
        {
            //col.GetComponent<IEnemy>().TakeDamage(CharacterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue());
            tmpenemy = null;
            
        }
    }

}
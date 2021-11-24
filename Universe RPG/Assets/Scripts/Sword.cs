using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public CharacterStats CharacterStats { get; set; }

    public int CurrentDamage { get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PeformAttack(int damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("Base_Attack");
    }

    public void PeformSpecialAttack()
    {
        animator.SetTrigger("Special_Attack");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<IEnemy>().TakeDamage(CharacterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue());
        }
    }

}
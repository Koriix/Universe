using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public List<BaseStat> Stats { get; set; }

    public void PeformAttack()
    {
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
            col.GetComponent<IEnemy>().TakeDamage(Stats[0].GetCalculatedStatValue());
        }
    }

}
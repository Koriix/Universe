using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    Item currentlyEquippedItem;
    IWeapon equippedWeapon;
    CharacterStats characterStats;
    Animator isWeapon;

    void Start()
    {
        characterStats = GetComponent<Player>().characterStats;
        isWeapon = GetComponent<Animator>();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if(EquippedWeapon != null)
        {
            UnequipWeapon();
        }
        else
        {
           isWeapon.SetBool("isWeapon", true);
        }
        
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        EquippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.Stats = itemToEquip.Stats;
        currentlyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(equippedWeapon.Stats);//EquippedWeapon.GetComponent<IWeapon>().Stats);
        Destroy(EquippedWeapon.transform.gameObject);//playerHand.transform.GetChild(0).gameObject);
        UIEventHandler.StatsChanged();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            PerformWeaponAttack();
            isWeapon.SetTrigger("Base_Attack");
        }
        else
        {
            isWeapon.ResetTrigger("Base_Attack");
        }

        if(Input.GetKeyDown(KeyCode.G))
            PerformWeaponAttack();
    }

    public void PerformWeaponAttack()
    {
        if(equippedWeapon != null)
            equippedWeapon.PeformAttack(CalculateDamage()); 
    }

    public void PerformWeaponSpecialAttack()
    {
        equippedWeapon.PeformSpecialAttack();
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue())
           + Random.Range(2, 8);
        damageToDeal += CalculateCrit(damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if(Random.value <= .10f)
        {
            int critDamage = (int)(damage * Random.Range(.5f, .75f));
            return critDamage;
        }
        return 0;
    }
}

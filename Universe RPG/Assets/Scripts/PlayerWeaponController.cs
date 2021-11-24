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

    void Start()
    {
        characterStats = GetComponent<Player>().characterStats;
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if(EquippedWeapon != null)
        {
            InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }
        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        EquippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.Stats = itemToEquip.Stats;
        currentlyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F));
            PerformWeaponAttack();

        if(Input.GetKeyDown(KeyCode.G));
            PerformWeaponAttack();
    }

    public void PerformWeaponAttack()
    {
        equippedWeapon.PeformAttack(CalculateDamage());
    }

    public void PerformWeaponSpecialAttack()
    {
        equippedWeapon.PeformSpecialAttack();
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.Power).GetCalculatedStatValue() * 2)
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

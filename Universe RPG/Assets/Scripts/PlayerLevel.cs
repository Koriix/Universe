using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int Level { get; set; }
    public int CurrentExp { get; set; }
    public int RequiredExp { get { return Level * 25; } }
    void Start()
    {
        CombatEvents.OnEnemyDeath += EnemyToExp;
        Level = 1;
    }

    public void EnemyToExp(IEnemy enemy)
    {
        GrantExp(enemy.Exp);
    }

    public void GrantExp(int amount)
    {
        CurrentExp += amount;
        while (CurrentExp >= RequiredExp)
        {
            CurrentExp -= RequiredExp;
            Level++;
        }

        UIEventHandler.PlayerLevelChanged();
    }

}

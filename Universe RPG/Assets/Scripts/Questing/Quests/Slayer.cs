using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slayer : Quest
{
    void Start()
    {
        QuestName = "New Monster Slayer in Universe";
        Description = "Kill some slime.";
        ItemReward = ItemDatabase.Instance.GetItem("potion_log");
        ExpReward = 100;

        Goals.Add(new KillGoal(this, 0, "Kill 5 Slimes", false, 0, 5));

        Goals.ForEach(g => g.Init());
    }
}

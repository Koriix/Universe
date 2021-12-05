using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : NPC
{
    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }

    public DialogueSystem dialogsys;

    [SerializeField]
    private GameObject quests;
    [SerializeField]
    private string questType;
    private Quest Quest { get; set; }
    public override void Interact()
    {
        if(!AssignedQuest && !Helped)
        {
            base.Interact();
            AssignQuest();
        }
        else if(AssignedQuest && !Helped)
        {
            CheckQuest();
        }
        else
        {
            DialogueSystem.Instance.AddnewDialogue(new string[] { "Thanks slayer!" }, name);
        }
    }

    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
    }

    void CheckQuest()
    {
        //Quest.CheckGoals();
        if(Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            DialogueSystem.Instance.AddnewDialogue(new string[] { "Thanks slayer! Take your reward." }, name);
        }
        else
        {
            DialogueSystem.Instance.AddnewDialogue(new string[] { "Come on kill those monsters!" }, name);
        } 
    }
}

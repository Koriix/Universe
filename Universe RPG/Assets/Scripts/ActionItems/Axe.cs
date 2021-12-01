using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : ActionItem
{
    public string[] dialogue;
    public override void Interact()
    {
        DialogueSystem.Instance.AddnewDialogue(dialogue, "Bill's Cursed Axe");
    }
}
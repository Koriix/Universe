using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public new string name;

    public override void Interact()
    {
        DialogueSystem.Instance.AddnewDialogue(dialogue, name);
    }
}

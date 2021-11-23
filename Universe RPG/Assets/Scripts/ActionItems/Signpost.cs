using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signpost : ActionItem
{
    public string[] dialogue;
    public override void Interact()
    {
        DialogueSystem.Instance.AddnewDialogue(dialogue, "Sign");
        Debug.Log("Interacting with Signpost");
    }
}

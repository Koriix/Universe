using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();
    }
   void Update()
   {
       if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
           animator.SetBool("isWalking", true);
            GetInteraction();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
        Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
   }

   void GetInteraction()
   {
       Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit interactionInfo;
       if(Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
       {
           playerAgent.updateRotation = true;
           GameObject interactedObject = interactionInfo.collider.gameObject;
           if(interactedObject.tag == "Enemy")
           {
               interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
           }
           else if(interactedObject.tag == "Interactable Object")
               interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
           else
           {
               playerAgent.stoppingDistance = 0;
               playerAgent.destination = interactionInfo.point;
           }
       }
   }
}

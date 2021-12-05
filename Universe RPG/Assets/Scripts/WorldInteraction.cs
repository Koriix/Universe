using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    private Animator animator;
    private Vector3 lastPos;
    private bool moving;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();
        lastPos = transform.position;
    }
   void Update()
   {
       Vector3 displacement = transform.position - lastPos;
       lastPos = transform.position;


       if(!Input.GetMouseButtonDown(1))
        {
           Debug.Log(displacement.magnitude);
           if(displacement.magnitude > 0.005 && displacement.magnitude < 1)
            {
                moving = true;
            } else {
                moving = false;
            }
        }

        if(!moving)
            animator.SetBool("isWalking", false);
        else
           animator.SetBool("isWalking", true);

       if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {

            GetInteraction();
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

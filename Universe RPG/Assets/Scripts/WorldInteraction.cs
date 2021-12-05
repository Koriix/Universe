using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour
{
    NavMeshAgent playerAgent;
    public Animator animator;
    private Vector3 lastPos;
    private bool moving;

    public float basespeed;
    public float runspeed;

    void Start()
    {
        //animator = GetComponent<Animator>();
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

           if(displacement.magnitude > 0.005 && displacement.magnitude < 0.09)
                moving = true;
            else
                moving = false;
        }

        if(moving)
            animator.SetBool("isWalking", true);
        else
           animator.SetBool("isWalking", false);

       if(Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            GetInteraction();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
                playerAgent.speed = runspeed;

        }else{
                playerAgent.speed = basespeed;

        }
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

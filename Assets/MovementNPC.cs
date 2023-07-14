using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementNPC : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartWalking();
    }

    void StartWalking()
    {
        // Set the "IsWalking" parameter to true
        animator.SetBool("IsWalking", true);

        // Schedule the character to stand after 5 seconds
       // Invoke("StandAfterWalking", 5f);
    }

   // void StandAfterWalking()
  //  {
        // Set the "IsWalking" parameter to false
       // animator.SetBool("IsWalking", false);

        // Schedule the character to nod after 2 seconds
        //Invoke("Nod", 2f);
   // }

   // void Nod()
   // {
        // Trigger the "IsNodding" parameter
     //   animator.SetTrigger("IsNodding");
   // }


}


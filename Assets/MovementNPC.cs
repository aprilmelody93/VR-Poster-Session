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

        // Start the coroutine to stop walking after the specified duration
      
    }

    
   
   

}


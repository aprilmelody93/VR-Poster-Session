using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovementNPC : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 2f;
    public float rotationSpeed = 5f;
    public float idleDuration = 2f;

    private Vector3[] targetPositions = new Vector3[]
    {
        new Vector3(3f, 0f, 0f),    // First target position
        new Vector3(-3f, 0f, 0f),   // Second target position
        // Add more target positions as needed
    };

    private int currentTargetIndex = 0;
    private bool isWalking = false;

    private void Start()
    {
        // Play initial idle animation
        StartCoroutine(IdleCoroutine());
    }

    private void Update()
    {
        if (!isWalking)
        {
            // Rotate character towards the current target position
            Quaternion targetRotation = Quaternion.LookRotation(targetPositions[currentTargetIndex] - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Move character towards the current target position
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

            // Check if the character has reached the current target position
            if (Vector3.Distance(transform.position, targetPositions[currentTargetIndex]) < 0.1f)
            {
                // Stop walking and play idle animation
                isWalking = false;
                StartCoroutine(IdleCoroutine());
            }
        }
    }

    private IEnumerator IdleCoroutine()
    {
        // Play idle animation
        animator.SetBool("IsWalking", false);

        yield return new WaitForSeconds(idleDuration);

        // Stop idle animation and start walking to the next target position
        animator.SetBool("IsWalking", true);
        currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
        isWalking = true;
    }
}


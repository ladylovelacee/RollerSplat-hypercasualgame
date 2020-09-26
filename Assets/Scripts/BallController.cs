using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float radius = default;
    [SerializeField] float moveSpeed = default;
    [SerializeField] int minSwipeRecognition = default;
    [Space]
    [SerializeField] float duration;

    //Determine direction
    Vector3 travelDirection;
    
    //Need for swipe mechanic
    Vector2 swipePosCurrentFrame;
    Vector2 swipePosLastFrame;
    Vector2 currentSwipe;

    //To save detected next collision front of ball
    Vector3 nextCollisionPosition;
    RaycastHit _hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameDatas.isMoving = false;
        GameDatas.isGameOver = false;
    }

    void FixedUpdate()
    {
        // Set the balls speed when it should travel
        if (GameDatas.isMoving && !GameDatas.isGameOver)
        {
           rb.velocity = travelDirection * moveSpeed;
        }

        Swipe();
        PaintingGrounds();
    }

    //Move through swipe
    void Swipe()
    {
        // Check if we have reached our destination
        if (nextCollisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                GameDatas.isMoving = false;
                
                travelDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;
            }
        }

        if (GameDatas.isMoving)
            return;

        // Swipe mechanism
        if (Input.GetMouseButton(0))
        {
            // Where is the mouse now?
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (swipePosLastFrame != Vector2.zero)
            {

                // Calculate the swipe direction
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;

                if (currentSwipe.sqrMagnitude < minSwipeRecognition) // Minium amount of swipe recognition
                    return;

                currentSwipe.Normalize(); // Normalize it to only get the direction not the distance (would fake the balls speed)

                // Up/Down swipe
                if (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                // Left/Right swipe
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }


            swipePosLastFrame = swipePosCurrentFrame;
        }
    }

    //Paint the ground
    void PaintingGrounds()
    {
        // Paint the ground
        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Coloring ground = hitColliders[i].transform.GetComponent<Coloring>();

            if (ground && !ground.isColored)
            {
                ground.Colored(BallColor.randomColor);
            }

            i++;
        }
    }

    //Detect next collision front of ball
    void SetDestination(Vector3 direction)
    {
        travelDirection = direction;

        if(Physics.Raycast(transform.position, travelDirection, out _hit, 100f))
        {
            nextCollisionPosition = _hit.point;
        }

        GameDatas.isMoving = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    bool isAlive = true;

    Rigidbody2D myRigidBody; 
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2d;
    BoxCollider2D myFeet;
    float gravityScaleAtStart;
    
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2d = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }


    void Update()
    {
        if(!isAlive) { return; }
        Run();
        Jump();
        flipSprite();
        die();

    }

    private void Run()
    {   
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to + 1 
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);    

    }


    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("ground"))){return;}


        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }  
    private void die()
    {
        if (myBodyCollider2d.IsTouchingLayers(LayerMask.GetMask("enemy" , "hazards")))
        {
            isAlive = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();           
        }


    }

    private void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

    }



}

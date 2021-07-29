using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //Movement Variables
   [Header("Movement Variables")]
    
    public float moveSpeed;

    //Jumping Variables
    [Header("Jumping Variables")]

    public float yForce;

    public float yGravity;

    public float maxGravity;

    public int jumpSpeed;

    public bool isJumping;

    //References
    [Header("References")]
    public DoubleJump doubleJump;
    public CharacterController myController;
    

    public GameObject playerModel;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
      myController = GetComponent<CharacterController>();
        doubleJump = GetComponent <DoubleJump>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }

        if(Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
    
        if(Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }
    
        if(Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isJumping)
            {
                Jump();
            }
            else
            {
                if(doubleJump.available)
                {
                    doubleJump.available = false;
                    Jump();
                }
            }
        }

        if(myController.isGrounded && yForce < 0)
        {
            isJumping = false;
            doubleJump.available = true;
            animator.SetBool("IsJumping",false);
        }
        
        if(!myController.isGrounded)
        {
            yForce = Mathf.Max(maxGravity, yForce + (yGravity * Time.deltaTime));
        }
        
        
        //Apply y-Force to a character
        myController.Move(Vector3.up * yForce * Time.deltaTime);

        if(IsIdle())
        {
            //Idle Animation
            animator.SetFloat("Speed",0);
        }
  
  
  
  
    }

    bool IsIdle()
    {
        if(!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            !isJumping)
        {
            return true;
        }
        return false;

    }
    void MoveLeft()
    {
        myController.Move(Vector3.left * Time.deltaTime * moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,270,0);
        animator.SetFloat("Speed",1);
    }

    void MoveRight()
    {
        myController.Move(Vector3.right * Time.deltaTime * moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,90,0);
        animator.SetFloat("Speed",1);
    }

    void MoveBack()
    {
        myController.Move(Vector3.back * Time.deltaTime * moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,180,0);
        animator.SetFloat("Speed",1);
    }

    void MoveForward()
    {
        myController.Move(Vector3.forward * Time.deltaTime * moveSpeed);
        playerModel.transform.eulerAngles = new Vector3(0,0,0);
        animator.SetFloat("Speed",1);
    }

    void Jump()
    {
        yForce = jumpSpeed;
        isJumping = true;
        animator.SetBool("IsJumping",true);
    }
}


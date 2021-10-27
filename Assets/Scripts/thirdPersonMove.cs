using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdPersonMove : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;
    Animator anim;
    public GameObject gameManager;
   
    public float speed = 6f;
    public float gravity = -9.81f;
    public bool isGrounded;
    public float groundDistance=0.4f;
    public float jumpHeight=3f;
    

    public float turnSmoothTime=0.1f;
    float turnSmoothVelocity;
    
    void Start()
    {
        anim=GetComponent<Animator>();
        anim.SetBool("jump",false);
    }

   
    void Update()
    {
        isGrounded=Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y= -2f;
            anim.SetBool("jump",false);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log(vertical);
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;


        if (direction.magnitude>= 0.1f)
        {
            float targetAngle=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle=Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation=Quaternion.Euler(0f,angle,0f);

            Vector3 moveDir=Quaternion.Euler(0f,targetAngle,0f)*Vector3.forward;
            controller.Move(moveDir.normalized*speed*Time.deltaTime);
            gameManager.GetComponent<AudioManager>().PlayStep();
            anim.SetBool("isRunning",true);
        }else{anim.SetBool("isRunning",false);}

        
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y= Mathf.Sqrt(jumpHeight*-2f*gravity*2);
                anim.SetBool("jump",true);
            }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            anim.SetTrigger("capoeira");
        }




         
    }
}

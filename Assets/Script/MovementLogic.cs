using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    private Rigidbody rb;
    public float walkspeed = 0.3f, runspeed = 1f, jumppower = 20f, fallspeed = 1f;
    private Transform PlayerOrientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    bool grounded = true;
    public Animator anim;    
    public bool TPSMode = true;
    public bool AimMode = false;
    public float HitPoints = 100f;
    
    public CameraLogic camlogic;    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        PlayerOrientation = this.GetComponent<Transform>();
        anim = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        jump();  
        Attack();
        AimModeAdjuster();

        if(Input.GetKey(KeyCode.F))
        {
            PlayerGetHit(100f);
        }
    }

    private void Movement()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = PlayerOrientation.forward * verticalInput + PlayerOrientation.right * horizontalInput;

        if (grounded && moveDirection != Vector3.zero)
        {          
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
                rb.AddForce(moveDirection.normalized * runspeed * 10f, ForceMode.Force);
            }
            else
            {
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);
                rb.AddForce(moveDirection.normalized * walkspeed * 10f, ForceMode.Force); 
            }
        } else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumppower, ForceMode.Impulse);
            grounded = false;
            anim.SetBool("Jump", true);
            
            
        } else if(!grounded) { 
            rb.AddForce(Vector3.down * fallspeed * rb.mass, ForceMode.Force);
        }
    }

    public void groundedchanger()
    {
        grounded = true;
        anim.SetBool("Jump", false);
    }

    public void PlayerGetHit(float damage)
    {
        Debug.Log("Player Receive Damage - " + damage);
        HitPoints = HitPoints - damage;

        if(HitPoints == 0f)
        {
            anim.SetBool("Death", true);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);            
        }
    }

    public void AimModeAdjuster()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("mouse1");
            if(AimMode)
            {
                TPSMode = true;
                AimMode = false;
                anim.SetBool("AimMode", false);
            }
            else if(TPSMode) 
            {
                TPSMode = false;
                AimMode = true;
                anim.SetBool("AimMode", true);                
            }
            camlogic.CameraModeChanger(TPSMode,AimMode);            
        }
    }
}
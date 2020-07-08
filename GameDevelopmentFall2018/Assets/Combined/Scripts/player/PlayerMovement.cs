using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Player player;
    private CharacterController CharControllerComponent;
    private Vector3 direction;

    public float moveSpeed = 1;
    public float runMultiplier = 2;
    public float jumpPower = 2;
    public float currentJumpPower;
    public float gravity = 7;
    public float mousespeed = 5;
    public float minmouseY = -45;
    public float maxmouseY = 45;
    private float inputH;
    private float inputV;
    private float RotationY = 0f;
    private float RotationX = 0f;
    private bool running = false;
    private bool run = false;
    private bool jump = false;
    private float mouseY = 0;
    private float mouseX = 0;
    void Start ()
    {
        player = GetComponent<Player>();
        CharControllerComponent = this.GetComponent<CharacterController>();

        currentJumpPower = jumpPower;
    }

    private void Update()
    {
        if(!player.Menu)
        {
            inputH = Input.GetAxis("Horizontal");
            inputV = Input.GetAxis("Vertical");
            jump = Input.GetButton("Jump");
            run = Input.GetKey(KeyCode.LeftShift);
            mouseY = Input.GetAxis("Mouse Y");
            mouseX = Input.GetAxis("Mouse X");
        }
        else
        {
            inputH = 0;
            inputV = 0;
            run = false;
            jump = false;
            mouseX = 0;
            mouseY = 0;
        }
    }
    // Update is called once per frame
    public void Move()
    {
        

        direction.y -= gravity * Time.deltaTime;   

        //Sets animation;
        player.anim.SetFloat("inputH",inputH);
        player.anim.SetFloat("inputV",inputV);
        player.anim.SetBool("run",running);

        if (CharControllerComponent.isGrounded)
        {
            direction = new Vector3(inputH, 0, inputV);
            if (jump)
            {
                Jump(currentJumpPower);
            }

            if (run)
            {
                running = true;
                direction.x *= runMultiplier;
                direction.z *= runMultiplier;
            }
            else
                running = false;
        }

        

        //this needs to move to playerController or input manager
        if (jump)
            player.anim.SetBool("jump", true);
        else
            player.anim.SetBool("jump", false);

        CharControllerComponent.Move(CharControllerComponent.transform.TransformDirection(direction * Time.deltaTime * moveSpeed));

        RotationX += player.fpsCam.transform.localEulerAngles.y + mouseX * mousespeed;
        RotationY -= mouseY * mousespeed;
        RotationY = Mathf.Clamp(RotationY, minmouseY, maxmouseY);
        this.transform.eulerAngles = new Vector3(0, RotationX, 0);
        player.fpsCam.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
    }

    public void Jump(float pwr)
    {
        Debug.Log("Jump");
        jump = false;
                          
       direction.y = pwr;
       currentJumpPower = jumpPower;

    }
    

    public float pushPower = 2.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.AddForce(pushDir * pushPower);

        if (hit.gameObject.GetComponent<ProgrammableObject>() != null)
        {
            ProgrammableObject hitPO = hit.gameObject.GetComponent<ProgrammableObject>();
            Debug.Log("Collided with PO");
            if (hitPO.teleportLink != null)
            {
                transform.position = hitPO.teleportLink.transform.position;
            }

            if (hitPO.activateLink != null)
            {
                if (hitPO.activateLink.luaScript.scriptText != null)
                {
                    hitPO.activateLink.RunScript();
                }
            }
//            if (hit.gameObject.GetComponent<ProgrammableObject>().bouncePwr != null)
//            {
//                
//                currentJumpPower = hit.gameObject.GetComponent<ProgrammableObject>().bouncePwr + gravity;
//                Jump(currentJumpPower);
//
//                //CharControllerComponent.Move(CharControllerComponent.transform.TransformDirection(direction * Time.deltaTime * moveSpeed));
//                this.jump = true;
//                Move();
//                            }
            
        }
    }
}

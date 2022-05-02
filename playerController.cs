using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    private Animator anim;
    private playerEquipment playerEquip;
    private playerStats playerStats;
    private spellCastHandler spellCastHandler;

    //Variables controlling player movement and speed
    private Rigidbody2D myRigidBody;
    private float lastMoveX;
    private float lastMoveY;
    private bool isMoving;
    public float currentSpeed;
    private bool isRunning;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        playerEquip = GetComponent<playerEquipment>();
        playerStats = GetComponent<playerStats>();
        spellCastHandler = getComponent<spellCastHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////HOTBAR FUNCTIONS////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerEquip.hotSwap(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerEquip.hotSwap(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerEquip.hotSwap(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerEquip.hotSwap(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerEquip.hotSwap(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playerEquip.hotSwap(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerEquip.hotSwap(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playerEquip.hotSwap(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            playerEquip.hotSwap(8);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (!isAttacking)
        {

            //This if statement controls spell casting
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                spellCastHandler.castSpell();
            }

            isMoving = false;

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentSpeed, myRigidBody.velocity.y);
                lastMoveX = Input.GetAxisRaw("Horizontal");
                isMoving = true;
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * currentSpeed);
                lastMoveY = Input.GetAxisRaw("Vertical");
                isMoving = true;
            }

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * (currentSpeed / 2));
            }
            else
            {
                currentSpeed = playerStats.moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, 0f);
            }

            if (Input.GetKeyDown(KeyCode.CapsLock))
            {
                isRunning = !isRunning;
            }

            if (isRunning)
            {
                currentSpeed = playerStats.moveSpeed * 2f;
            }
            else
            {
                currentSpeed = playerStats.moveSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            myRigidBody.velocity = new Vector2(0f, 0f);
        }
        /*if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isAttacking = false;
        }*/

        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("lastMoveX", lastMoveX);
        anim.SetFloat("lastMoveY", lastMoveY);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isAttacking", isAttacking);
    }

    void endAnim()
    {
        isAttacking = false;
    }
}

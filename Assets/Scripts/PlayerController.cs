//=======================================================
//Author: Giovanni Cuadra
//Date  : 09-09-2026
//Desc  : Handles all player interaction with World
//=======================================================

using System.Runtime.CompilerServices;
using UnityEngine;
//this is required for loading a scene
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    //we need to access the rigidbody2d on the player
    private Rigidbody2D player_rb;

    //we need to have a variable to control the speed of the player
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;
    //how many jumps the player can perform
    private int numJumps;
    //max number of jumps can perform until they need to touch the ground
    [SerializeField]
    private int maxNumJumps;
    [SerializeField]

    //where on the player the hat should be placed.
    public GameObject doubleJumpHatLocation;

    void Start()
    {
        //we need to set the player rigidbody variable
        //I can only get this component because the rigidbody2d is attached to the player
        //and this script is also attached to this player
        player_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerLateral();
        jump();
    }

    private void movePlayerLateral()
    {
        //if A/D/<-/-> are pressed move the player accordingly
        //"Horizontal" is defined in the input section fo the project settings
        //the line below will return:
        //0 - no button pressed
        //1 - right arrow or d
        //2 - left arror or a pressed
        float inputHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        flipPlayerSprite(inputHorizontal);
        player_rb.linearVelocity = new Vector2(inputHorizontal * movementSpeed, player_rb.linearVelocityY);
    }

    private void flipPlayerSprite(float input)
    {
        //this function will make the player face the direction they are moving
        if(input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void jump()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Space) && numJumps <= maxNumJumps)
        {
            player_rb.linearVelocity = new Vector2(player_rb.linearVelocity.x, jumpForce);
            numJumps++;
        }
    }

    //This is a prebuilt function that will detect collisions
    //in order to detect collisions both of the following must be true:
    //1. both objects need to have a collider
    //2. one of the objects needs a rigidbody
    //3 different collisions: onenter, onexit, and onstay

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OB"))
        {
            Debug.Log("Restart Level");
            SceneManager.LoadScene("SampleScene");
        }    
        else if (collision.gameObject.CompareTag("Ground"))
        {
            numJumps = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJump"))
        {
            maxNumJumps = 2;
            GameObject hat = collision.gameObject;
            equipDoubleJumpHat(hat);
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            Destroy(collision.gameObject);
        }

    }

    private void equipDoubleJumpHat(GameObject hat)
    {
        hat.transform.position = doubleJumpHatLocation.transform.position;
        hat.transform.SetParent(this.gameObject.transform);
    }

    //wall jumps using OnCollisionStay2D
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Ground"))
    //    {
    //        numJumps = 1;
    //    }
    //}
}

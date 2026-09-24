using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private bool horizontalMovement;
    [SerializeField]
    private bool verticalMovement;

    private bool moveLeft;

    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        movePlatform();
    }
    
    private void movePlatform()
    {
        //check to see if platforms needs to move horizontal or vertical
        if(horizontalMovement)
        {
            //move platform to the left
            if (moveLeft)
            {
                //This type of movement is set off of framerate
                //we need to get this movement in units of time
                //This needs to be multiplied by Time.delaTime so that we are moving
                //the object based off time and not framerate.
                //We do not need this when moving the player because velocity is already in a time metric.
                transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            }
            //move platform to the right
            else
            {
                //This type of movement is set off of framerate
                //we need to get this movement in units of time
                //This needs to be multiplied by Time.delaTime so that we are moving
                //the object based off time and not framerate.
                //We do not need this when moving the player because velocity is already in a time metric.
                transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MoveWallLeftBound"))
        {
            moveLeft = false;
        }
        else if (collision.gameObject.CompareTag("MoveWallRightBound"))
        {
            moveLeft = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //we want to parent the platform to the player
            //This will apply all movement from the platform to the player
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

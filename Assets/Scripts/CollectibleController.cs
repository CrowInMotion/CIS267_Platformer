using Unity.VisualScripting;
using UnityEngine;

//attached to collectable used to move collectable and handle interactions
public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private bool magnetToPlayer = false;
    private int magnetSpeed;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setRandomSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        moveToPlayer();
    }

    private void moveLeft()
    {
        rb.linearVelocity = new Vector2(speed * -1, 0);
    }

    private void moveToPlayer()
    {
        //if the collectible has collided with AbsorbField, this magnetizes it towards the player
        if (magnetToPlayer)
        {
            //uses the rigid body of the collectible as well as the player position to determine start and end points
            Vector2 currentPos = rb.position;
            Vector2 playerPos = player.transform.position;
            
            //forces the collectible to move towards the player at half a frame speed, might change to time later
            transform.position = Vector2.MoveTowards(currentPos, playerPos, 0.5f);
        }
    }

    public void setRandomSpeed()
    {
        float minSpeed = 3;
        float maxSpeed = 10;
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OB"))
        {
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("AbsorbField"))
        {
            //when colliding with AbsorbField, this grabs its parent (the player)
            player = collision.transform.parent.gameObject;
            
            //sets the parent of the collectible as the player
            gameObject.transform.SetParent(player.transform);
            magnetToPlayer = true;
        }
    }
}
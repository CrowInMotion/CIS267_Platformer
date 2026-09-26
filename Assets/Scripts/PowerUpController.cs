using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private Rigidbody2D absorbField_rb;

    public GameObject mouth;
    public GameObject openMouth;
    public GameObject fullMouth;
    public GameObject blush;
    public GameObject leftBlush;
    public GameObject rightBlush;
    public GameObject absorbField;
    public GameObject eyesDefault;
    public GameObject eyesBM;

    [SerializeField]
    private bool bigMouthEnabled;
    [SerializeField]
    private int bigMouthCooldown;
    [SerializeField]
    private int bigMouthAbsorbTime;
    [SerializeField]
    private int absorbFieldSize;

    private float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        absorbField_rb = absorbField.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        eat();
    }

    private void eat()
    {
        if (bigMouthEnabled == true)
        {
            //sets visual change for BigMouth powerup (eyes and blush)
            eyesBM.SetActive(true);
            eyesDefault.SetActive(false);
            activateBlush();

            //time is only 0 the first time powerup is collected
            //if time is less than cooldown then keep counting
            if (time != 0 && time < bigMouthCooldown)
            {
                //full mouth shown during cooldown
                if (time > bigMouthAbsorbTime)
                {
                    changeMouthFull();
                }

                time += Time.deltaTime;
            }

            //if not in cooldown and E is pressed, change mouth and begin absorbing nearby collectibles
            else if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                changeMouthOpen();
                absorbCollectibles();
            }

            //default is set when cooldown is ready
            else
            {
                fullMouth.SetActive(false);
                openMouth.SetActive(false);
                mouth.SetActive(true);
                blush.SetActive(true);
            }
        }
    }

    //shows the open mouth during collectible absorbing and starts the cooldown timer
    private void changeMouthOpen()
    {
        mouth.SetActive(false);
        fullMouth.SetActive(false);
        openMouth.SetActive(true);

        time = 0f;
        time += Time.deltaTime;
    }
    //visual indicator for the player
    private void absorbCollectibles()
    {
        absorbField.SetActive(true);
        //this allows the scale of the field to change in case of later upgrade options are added
        absorbField.transform.localScale = new Vector3(absorbFieldSize,absorbFieldSize, absorbFieldSize);
    }

    //cooldown face is set
    private void changeMouthFull()
    {
        absorbField.SetActive(false);
        blush.SetActive(false);
        openMouth.SetActive(false);
        mouth.SetActive(false);
        fullMouth.SetActive(true);
    }

    //shows blush, this allows me to disable blush during cooldown because it looked bad
    private void activateBlush()
    {
        if (blush != null)
        {
            blush.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //destroys the powerup in the level once collected
        if (collision.gameObject.CompareTag("BigMouth"))
        {
            bigMouthEnabled = true;
            //activateBlush();
            Destroy(collision.gameObject);
        }
    }
}

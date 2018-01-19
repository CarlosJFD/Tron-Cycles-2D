using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour {
    
    public float speed = 25;
    
    public GameObject wallPrefab;
    
    Collider2D wall;
    
    Vector2 lastWallEnd;

    bool isCycleInitiate = false;

    public GameObject boom;
    public AudioSource boomSound;

    public float initialRotation;

    public Vector2 initialVectorDirection;

    public string objectName;

    public GameObject[] rivals;

    public Text textWinner;

    public LoadSceneEndGame loadSceneEndGame;

    public InteractiveElement buttonUp;
    public InteractiveElement buttonDown;
    public InteractiveElement buttonLeft;
    public InteractiveElement buttonRight;
    public InteractiveElement lastButtonPressed;

    // Use this for initialization
    void Start ()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, initialRotation);
        Invoke("initiateCycle", 3.0f);
    }

    void initiateCycle()
    {
        isCycleInitiate = true;
        GetComponent<Rigidbody2D>().velocity = initialVectorDirection * speed;
        spawnWall();
    }

    // Update is called once per frame
    void Update () {

        if (isCycleInitiate)
        {
            if (buttonUp.pressed)
            {
                if (buttonDown != lastButtonPressed)
                {
                    lastButtonPressed = buttonUp;
                    this.transform.rotation = Quaternion.Euler(0, 0, 270);
                    GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
                    spawnWall();
                }
            }
            else if (buttonDown.pressed)
            {
                if (buttonUp != lastButtonPressed)
                {
                    lastButtonPressed = buttonDown;
                    this.transform.rotation = Quaternion.Euler(0, 0, 90);
                    GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
                    spawnWall();
                }
            }
            else if (buttonRight.pressed)
            {
                if (buttonLeft != lastButtonPressed)
                {
                    lastButtonPressed = buttonRight;
                    this.transform.rotation = Quaternion.Euler(0, 0, 180);
                    GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
                    spawnWall();
                }
            }
            else if (buttonLeft.pressed)
            {
                if (buttonRight != lastButtonPressed)
                {
                    lastButtonPressed = buttonLeft;
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
                    spawnWall();
                }
            }

            fitColliderBetween(wall, lastWallEnd, transform.position);


            bool winner = true;
            for (int i = 0; i < rivals.Length; i++)
            {
                if (rivals[i] != null)
                {
                    winner = false;
                }
            }


            if (winner)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Camera.main.orthographicSize = 8;
                Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 4, -10);

                textWinner.text = objectName + "\nWIN!!";

                loadSceneEndGame.ActivateLoad();
            }
        }
    }

    void spawnWall()
    {
        // Save last wall's position
        lastWallEnd = transform.position;

        // Spawn a new Lightwall
        GameObject g = (GameObject)Instantiate(wallPrefab, transform.position, Quaternion.identity);
        g.name = "Wall";
        wall = g.GetComponent<Collider2D>();
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        // Calculate the Center Position
        co.transform.position = a + (b - a) * 0.5f;

        // Scale it (horizontally or vertically)
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        // Not the current wall?
        if (co != wall)
        {
            Destroy(gameObject);
            Instantiate(boom, transform.position, Quaternion.identity);
            boomSound.Play();
        }
    }
}

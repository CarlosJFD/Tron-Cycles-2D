using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IA : MonoBehaviour {
    
    public float speed = 25;
    
    public GameObject wallPrefab;
    
    Collider2D wall;
    
    Vector2 lastWallEnd;

    bool isCycleInitiate = false;

    public GameObject boom;
    public AudioSource boomSound;

    public float initialRotation;
    public Vector2 initialVectorDirection;

    public string moveDirection;

    RaycastHit2D hit;

    public string objectName;

    public GameObject[] rivals;

    public Text textWinner;

    public LoadSceneEndGame loadSceneEndGame;

    // Use this for initialization
    void Start()
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
    void Update()
    {
        if (isCycleInitiate)
        {
            updateRaycast();

            if (hit.transform != null)
            {
                if (moveDirection == "up" || moveDirection == "down")
                {
                    hitUpAndDown();
                }
                else if (moveDirection == "left" || moveDirection == "right")
                {
                    hitLeftAndRight();
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

                textWinner.text = objectName + "\nWINS!!";

                loadSceneEndGame.ActivateLoad();
            }
        }
    }

    void hitUpAndDown()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Vector2.right, 10);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(this.transform.position.x - 2, this.transform.position.y, this.transform.position.z), Vector2.left, 10);

        if (hitRight.transform != null && hitLeft.transform != null)
        {
            if (hitRight.distance > hitLeft.distance)
            {
                moveRight();
                updateRaycast();
            }
            else if (hitRight.distance < hitLeft.distance)
            {
                moveLeft();
                updateRaycast();
            }
            else
            {
                var randomInt = Random.Range(0, 2);
                if (randomInt == 0)
                {
                    moveLeft();
                    updateRaycast();
                }
                else if (randomInt == 1)
                {
                    moveRight();
                    updateRaycast();
                }
            }
        }
        else if (hitRight.transform != null)
        {
            moveLeft();
            updateRaycast();
        }
        else if (hitLeft.transform != null)
        {
            moveRight();
            updateRaycast();
        }
        else
        {
            var randomInt = Random.Range(0, 2);
            if (randomInt == 0)
            {
                moveLeft();
                updateRaycast();
            }
            else if (randomInt == 1)
            {
                moveRight();
                updateRaycast();
            }
        }
    }

    void hitLeftAndRight()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Vector2.up, 10);
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector3(this.transform.position.x, this.transform.position.y - 2, this.transform.position.z), Vector2.down, 10);

        if (hitUp.transform != null && hitDown.transform != null)
        {
            if (hitUp.distance > hitDown.distance)
            {
                moveUp();
                updateRaycast();
            }
            else if (hitUp.distance < hitDown.distance)
            {
                moveDown();
                updateRaycast();
            }
            else
            {
                var randomInt = Random.Range(0, 2);
                if (randomInt == 0)
                {
                    moveUp();
                    updateRaycast();
                }
                else if (randomInt == 1)
                {
                    moveDown();
                    updateRaycast();
                }
            }
        }
        else if (hitUp.transform != null)
        {
            moveDown();
            updateRaycast();
        }
        else if (hitDown.transform != null)
        {
            moveUp();
            updateRaycast();
        }
        else
        {
            var randomInt = Random.Range(0, 2);
            if (randomInt == 0)
            {
                moveUp();
                updateRaycast();
            }
            else if (randomInt == 1)
            {
                moveDown();
                updateRaycast();
            }
        }
    }

    void moveRight()
    {
        moveDirection = "right";
        this.transform.rotation = Quaternion.Euler(0, 0, 180);
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        spawnWall();
    }

    void moveLeft()
    {
        moveDirection = "left";
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
        spawnWall();
    }

    void moveUp()
    {
        moveDirection = "up";
        this.transform.rotation = Quaternion.Euler(0, 0, 270);
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        spawnWall();
    }

    void moveDown()
    {
        moveDirection = "down";
        this.transform.rotation = Quaternion.Euler(0, 0, 90);
        GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
        spawnWall();
    }

    void updateRaycast()
    {
        if (moveDirection == "up")
        {
            hit = Physics2D.Raycast(new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Vector2.up, 1);
        }
        else if (moveDirection == "left")
        {
            hit = Physics2D.Raycast(new Vector3(this.transform.position.x - 3, this.transform.position.y, this.transform.position.z), Vector2.left, 1);
        }
        else if (moveDirection == "right")
        {
            hit = Physics2D.Raycast(new Vector3(this.transform.position.x + 3, this.transform.position.y, this.transform.position.z), Vector2.right, 1);
        }
        else if (moveDirection == "down")
        {
            hit = Physics2D.Raycast(new Vector3(this.transform.position.x, this.transform.position.y - 3, this.transform.position.z), Vector2.down, 1);
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

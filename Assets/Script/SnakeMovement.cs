using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private float stepSize = 1.0f; // Size of one grid step
    public float moveRate = 0.5f; // Time between moves in seconds
    private Vector2 direction = Vector2.up; // Initial direction
    public bool isGameStarted = false; // Check if the game has started
    private float timer; // Timer to track movement intervals
    private List<Transform> _segment;
    public Transform snakebody;
    [SerializeField] GameObject GameoverUI;
    private bool ignoreBodyCollision = false; // Flag to ignore collision with body
    private int stepsAfterEating = 0; // Counter to track steps after eating
   

    private void Start()
    {
       
        GameoverUI.SetActive(false);
        _segment = new List<Transform>();
        _segment.Add(transform); // Adding the head to the segment list
    }

    private void Update()
    {
        if (isGameStarted)
        {
            HandleInput();
            timer += Time.deltaTime;
            if (timer >= moveRate)
            {
                timer = 0f;
                Move();
                if (stepsAfterEating > 0)
                {
                    stepsAfterEating--;
                    if (stepsAfterEating == 0)
                    {
                        ignoreBodyCollision = false;
                    }
                }
            }
        }
        SnakeWrapping();
    }

    void SnakeWrapping()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        float rightSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x;
        float leftSideOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
        float topOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        float bottomOfScreenInWorld = Camera.main.ScreenToWorldPoint(new Vector2(0f, 0f)).y;

        if (screenPos.x <= 0 && direction == Vector2.left)
        {
            transform.position = new Vector2(rightSideOfScreenInWorld, transform.position.y);
        }
        else if (screenPos.x >= Screen.width && direction == Vector2.right)
        {
            transform.position = new Vector2(leftSideOfScreenInWorld, transform.position.y);
        }
        else if (screenPos.y >= Screen.height && direction == Vector2.up)
        {
            transform.position = new Vector2(transform.position.x, bottomOfScreenInWorld);
        }
        else if (screenPos.y <= 0 && direction == Vector2.down)
        {
            transform.position = new Vector2(transform.position.x, topOfScreenInWorld);
        }
    }

    private void HandleInput()
    {
        // Ensures the snake can only make orthogonal turns and not reverse on itself
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && direction != Vector2.left)
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && direction != Vector2.right)
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && direction != Vector2.down)
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && direction != Vector2.up)
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void Move()
    {
        for (int i = _segment.Count - 1; i > 0; i--)
        {
            _segment[i].position = _segment[i - 1].position;
        }
        // Move the snake by one grid step
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x * stepSize,
            Mathf.Round(transform.position.y) + direction.y * stepSize,
            0.0f
        );
    }

    public void StartGame()
    {
        SoundManager.Instance.playclip(AudioType.Levelload);
        isGameStarted = true;
    }


    public void Grow()
    {
        Transform segment = Instantiate(snakebody);
        if (_segment.Count > 0)
        {
            segment.position = _segment[_segment.Count - 1].position;
        }
        else
        {
            // If the list is empty, position the segment at the snake's current position or another default position
            segment.position = transform.position;
        }
        _segment.Add(segment);

        ignoreBodyCollision = true; // Set flag to ignore body collision
        stepsAfterEating = 1; // Set steps counter after eating
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();

        }
        else if (other.tag == "Body" && !ignoreBodyCollision)
        {
            Die();
        }
    }

    private void Die()
    {
        SoundManager.Instance.playclip(AudioType.death);
        Debug.Log("Die Game Over");
        GameoverUI.SetActive(true);
        isGameStarted = false;
    }
}

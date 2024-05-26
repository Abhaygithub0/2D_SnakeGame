using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private float stepSize = 1.0f;
    public float moveRate = 0.5f;
    private Vector2 direction = Vector2.up;
    public bool isGameStarted = false;
    private float timer;
    private List<Transform> _segment;
    public Transform snakebody;
    [SerializeField] GameObject GameoverUI;
    private bool ignoreBodyCollision = false;
    private int stepsAfterEating = 0;
    [SerializeField] private float immunityDuration = 3f;


    private void Start()
    {

        GameoverUI.SetActive(false);
        _segment = new List<Transform>();
        _segment.Add(transform);

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
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
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

            segment.position = transform.position;
        }
        _segment.Add(segment);

        ignoreBodyCollision = true;
        stepsAfterEating = 1;
    }
    public void MinusGrow()
    {
        if (_segment.Count > 1)
        {
            Transform lastSegment = _segment[_segment.Count - 1];
            _segment.Remove(lastSegment);
            Destroy(lastSegment.gameObject);
        }
        else
        {

            Debug.Log("No more segments to remove!");
        }
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
        else if (other.tag == "PosionFood")
        {
            MinusGrow();
        }
        else if (other.tag == "PowerFood")
        {
            SetImmunity(immunityDuration);
        }
    }

    private void Die()
    {
        SoundManager.Instance.playclip(AudioType.death);
        Debug.Log("Die Game Over");
        GameoverUI.SetActive(true);
        isGameStarted = false;
    }
    private void SetImmunity(float duration)
    {
        ignoreBodyCollision = true;
        StartCoroutine(RemoveImmunityAfterDelay(duration));
    }

    private IEnumerator RemoveImmunityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ignoreBodyCollision = false;
    }

}
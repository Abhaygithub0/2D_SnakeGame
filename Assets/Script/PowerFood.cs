using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFood : MonoBehaviour
{
    [SerializeField] private int scoreLoss = 10; // Amount of score to lose
    [SerializeField] private Food foodComponent; // Reference to the Food component
    [SerializeField] private SnakeMovement snake; // Reference to the SnakeMovement component
    [SerializeField] private Score score; // Reference to the Score component
    [SerializeField] private Collider2D foodCollider;
    [SerializeField] private float timeDelay = 5f;

    private void Awake()
    {
       UnactiveFood();
        
    }

    private void Start()
    {
        StartCoroutine(DelayActivation(timeDelay));
    }

    void UnactiveFood()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    private void ActivateFood()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SnakeMovement>() != null)
        {
            Debug.Log("OnTriggerEnter2D: Snake collided");
            ConsumePowerFood();
            StartCoroutine(DelayActivation(timeDelay));
        }
    }
    
    private void ConsumePowerFood()
    {
        score.incrementvalue(scoreLoss);
        UnactiveFood();
    }

    public void RandomizePosition()
    {
        Bounds bounds = foodCollider.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private IEnumerator DelayActivation(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateFood();
        RandomizePosition();
    }
    
}

using System.Collections;
using UnityEngine;

public class PosionFood : MonoBehaviour
{
    [SerializeField] private int scoreLoss = 10; // Amount of score to lose
    [SerializeField] private Food foodComponent; // Reference to the Food component
    [SerializeField] private SnakeMovement snake; // Reference to the SnakeMovement component
    [SerializeField] private Score score; // Reference to the Score component
    [SerializeField] private Collider2D foodCollider;
    [SerializeField] private float timeDelay = 5f;
    [SerializeField] private float TimeRun = 3f;
    bool IsFoodActive = false;

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
        IsFoodActive = false;
    }
    private void ActivateFood()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        IsFoodActive=true;
        StartCoroutine(CheckActivation(TimeRun));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SnakeMovement>() != null)
        {
            SoundManager.Instance.playclip(AudioType.pickableheavy);
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
    private IEnumerator CheckActivation(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (IsFoodActive == true)
        {
            UnactiveFood();
            StartCoroutine(DelayActivation(timeDelay));
        }
    }
}

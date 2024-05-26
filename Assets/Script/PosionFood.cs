using System.Collections;
using UnityEngine;

public class PosionFood : MonoBehaviour
{
    [SerializeField] private Collider2D foodCollider;
    [SerializeField] private float timeDelay = 5f; 
    [SerializeField] private Score score;
    public int scoreNumber = 0;

    private void Start()
    {
      
       RandomizePosition();
    }

    private void RandomizePosition()
    {
      
        Bounds bounds = foodCollider.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
       
    }

    private void ActivateFood()
    {
       
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
       
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SnakeMovement>() != null)
        {
            score.incrementvalue(scoreNumber);
            SoundManager.Instance.playclip(AudioType.pickableheavy);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            StartCoroutine(DelayActivation(timeDelay));
        }
    }

    private IEnumerator DelayActivation(float delay)  
    {
        
        yield return new WaitForSeconds(delay);
        RandomizePosition();
        ActivateFood();
    }
}

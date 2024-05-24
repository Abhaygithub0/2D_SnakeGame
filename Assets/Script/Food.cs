using System;
using System.Collections;
using UnityEngine;



public class Food : MonoBehaviour
{
    [SerializeField] private Collider2D foodcollider;
    [SerializeField] private float timedelay = 0;
    [SerializeField] private  Score Score;
    
    
   
    private void Start()
    {
        RandomizePosition();
      
    }

    private void RandomizePosition()
    {
       
        Bounds bounds = foodcollider.bounds;

        float x = UnityEngine. Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        
    }

    private void activefood()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
  
   

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SnakeMovement>() != null)
        {
            Score.incrementvalue(10);
            SoundManager.Instance.playclip(AudioType.pickablelight);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
          
            StartCoroutine(DelayedRandomizePosition(timedelay));
           
            

        }
    }

    private IEnumerator DelayedRandomizePosition(float delay)
    {
        yield return new WaitForSeconds(delay);
        RandomizePosition();
        activefood();

        
    }

}






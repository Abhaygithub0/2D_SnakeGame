using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



public class Food : MonoBehaviour
{
    [SerializeField] private Collider2D foodcollider;
    [SerializeField] private  ScoreDisplay Score;
    protected bool IsFoodActive = false;
    [SerializeField] protected float TimeDelay = 5f;
    [SerializeField] protected float TimeRun = 3f;
    [SerializeField] protected float CheckTimeRun = 3f;
    [SerializeField] protected int ScoreValue;


    protected void Awake()
    {
        UnactiveFood();

    }
    protected void Start()
    {
        StartCoroutine(DelayedRandomizePosition(TimeRun));

    }

    protected void RandomizePosition()
    {
       
        Bounds bounds = foodcollider.bounds;

        float x = UnityEngine. Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        
    }
    protected void ConsumePowerFood()
    {
       Score .incrementvalue(ScoreValue);
        UnactiveFood();
    }

    protected void UnactiveFood()
    {
        IsFoodActive = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
    protected void ActivateFood()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        IsFoodActive = true;
        StartCoroutine(CheckActivation(CheckTimeRun));
       
    }

    protected IEnumerator DelayedRandomizePosition(float delay)
    {
        yield return new WaitForSeconds(delay);
        RandomizePosition();
        ActivateFood();

    }
    protected IEnumerator CheckActivation(float delay)
    {

        yield return new WaitForSeconds(delay);
        if (IsFoodActive == true)
        {
            UnactiveFood();
            StartCoroutine(DelayedRandomizePosition(TimeDelay));
        }
    }
}
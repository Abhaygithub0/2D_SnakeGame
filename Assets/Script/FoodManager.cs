using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private Foodthing[] foodthing;
    [SerializeField] private Collider2D collidedr;
   public void posionfood()
    {
        Debug.Log("Player collide with posion food");
        DelayTime(2f);
    }
    public void Coinfood()
    {
        Debug.Log("Player collide with posion food");
        DelayTime(1f);
    }
    public void Normalfood()
    {
        Debug.Log("Player collide with posion food");
        DelayTime(3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private IEnumerator DelayTime(float delay) 
    {
        yield return new WaitForSeconds(delay);
    }
  
}

[Serializable]
public class Foodthing
{
    public Foodtype foodtype;
    public float delaytime;
}
public enum Foodtype
{
    PosionFood,
    CoinFood,
    NormalFood,
}


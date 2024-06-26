using System.Collections;
using UnityEngine;

public class PosionFood :Food
{
    new void Awake()
    {
        base.Awake();
    }
    new void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SnakeMovement>() != null)
        {
            SoundManager.Instance.playclip(AudioType.pickableheavy);
            ConsumePowerFood();
            StartCoroutine(DelayedRandomizePosition(TimeDelay));
        }

    }
}

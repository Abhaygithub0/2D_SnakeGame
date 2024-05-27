using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : SnakeMovement
{
    // Start is called before the first frame update
   new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
   new void Update()
    {
        base.Update();
    }
        protected override void HandleInput()
        {
        // Existing input handling logic for WASD
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
            transform.rotation = Quaternion.Euler(0, 0, 180);
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

}



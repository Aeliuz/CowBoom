using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rb2D;
    Vector2 input;

    Vector2 position;
    public float accelarate = 5f;
    public float deAccalarate;

    public float maxSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frames
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.sqrMagnitude > 0)
        {
            input.Normalize();
        }

        velocity += input * accelarate * Time.deltaTime;

        if(velocity.sqrMagnitude > maxSpeed * maxSpeed) 
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;

        }

        if(input .sqrMagnitude == 0)
        {
            velocity *= 1 - deAccalarate * Time.deltaTime;
        }

        position += velocity * Time.deltaTime;


        rb2D.velocity = velocity;


        

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bullet bulletprefab;
    public Rigidbody2D rb;
    public bool thursting;
    public float speed=1.0f;
    public float turningspeed=1.0f;
    public float turningDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        thursting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turningDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turningDirection = -1.0f;
        }
        else
        {
            turningDirection = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
    
        }
    }

    private void FixedUpdate()
    {
        if (thursting)
        {
            rb.AddForce(transform.up * speed);
        }
        if(turningDirection != 0)
        {
            rb.AddTorque(turningDirection * turningspeed);
        }
    }

    void Shoot()
    {
        bullet bullet = Instantiate(bulletprefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid")
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}

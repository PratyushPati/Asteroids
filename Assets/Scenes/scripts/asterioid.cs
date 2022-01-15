using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asterioid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed=50.0f;
    public float lifetime = 30f;

    public SpriteRenderer sr;
    public Rigidbody2D rb;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        sr.sprite = sprites[Random.Range(0, sprites.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f);
        transform.localScale = Vector3.one * size;

        rb.mass = size*2.0f;
    }
    public void settrajctory(Vector2 direction)
    {
        rb.AddForce(direction * speed);

        Destroy(gameObject, lifetime);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (size * 0.5f > minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }
    private void CreateSplit()
    {
        Vector2 position= transform.position;
        position += Random.insideUnitCircle * 0.5f;

        asterioid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;
        half.settrajctory(Random.insideUnitCircle.normalized*speed);
    }
}

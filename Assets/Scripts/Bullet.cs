using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 30;
    public float lifeTime = 2;
    public Vector2 damageRange = new Vector2(10, 20);

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var damage = Random.Range(damageRange.x, damageRange.y);

        //TODO: Apply damage to other.gameObject

        print($"Dealt {damage} damage to {other.gameObject.name}");

        Destroy(gameObject);
    }
}
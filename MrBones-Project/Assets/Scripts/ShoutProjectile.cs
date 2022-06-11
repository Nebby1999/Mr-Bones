using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutProjectile : MonoBehaviour
{
    public float speed;
    public int maxBounces;
    public Vector2 direction;
    public Rigidbody2D RigidBody2D { get; private set; }

    private void Awake()
    {
        RigidBody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        RigidBody2D.velocity = direction * speed;
    }
    private void FixedUpdate()
    {
        RigidBody2D.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Vector2.Reflect(direction, collision.GetContact(0).normal);
    }
}

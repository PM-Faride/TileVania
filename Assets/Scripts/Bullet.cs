using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    float xSpeed;
    PlayerMovement player;
    Rigidbody2D myRigidBody;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = Mathf.Sign(player.transform.localScale.x) * bulletSpeed;
        //Debug.Log(xSpeed);
        //transform.localScale = new Vector3(Mathf.Sign(xSpeed) * -1, transform.localScale.y, 1);
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = new Vector2(xSpeed, 0);
    }

    void Update()
    {
        //myRigidBody.velocity = new Vector2(xSpeed, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
           Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

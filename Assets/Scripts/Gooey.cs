using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gooey : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidBody;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        // mese ine k niro behesh mide faqatttttttt in tafavot has k age inja bashe bad k speed avaz mishe in avaz nemishe
        //myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipTheEnemy();
    }

    void FlipTheEnemy()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed) * transform.localScale.x, transform.localScale.y);
    }
}

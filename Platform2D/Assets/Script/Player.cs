using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Run();

    }

    void Update()
    {
        Jump();

        
    }


    void Run()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(new Vector2(h * speed * Time.deltaTime, 0), ForceMode2D.Impulse);

        if (rigid.velocity.x > 20)
            rigid.velocity = new Vector2(20, rigid.velocity.y);


        if (Input.GetAxisRaw("Horizontal") < 0)
            render.flipX = true;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            render.flipX = false;


        if (Input.GetButton("Horizontal"))
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float maxSpeed;
    [SerializeField] float health;


    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer render;

    public bool isJump;

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

        rigid.AddForce(Vector2.right * h * speed * Time.deltaTime, ForceMode2D.Impulse);




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
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
        }
        else
        {
            anim.SetBool("isJump", false);
        }

        RaycastHit2D rayCast;

        rayCast = Physics2D.Raycast(transform.position, Vector2.down * 0.4f, 0.5f, LayerMask.GetMask("Plat"));

        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);

        if (rayCast)
        {
            isJump = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            health -= 30;
        }



    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpPower;
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

        
        
        Debug.DrawRay(rigid.position, Vector2.down, Color.green);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Plat"));


        if (rigid.velocity.y < 0)
        {
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJump", false);
                    isJump = false;
                }
            }
        }
    }

    void Update()
    {
        Jump();

        if (Input.GetAxisRaw("Horizontal") < 0)
            render.flipX = true;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            render.flipX = false;

        if (Input.GetButton("Horizontal"))
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);

        if(Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);


    }


    void Run()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h , ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if( rigid.velocity.x < maxSpeed * -1)
        {
            rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
        }




    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
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

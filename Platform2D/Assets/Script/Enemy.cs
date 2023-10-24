using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator enemyAnim;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        Think();

        Invoke("Think", 5);
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(nextMove * 0.2f + rigid.position.x, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Plat"));

        if(rayHit.collider == null)
        {
            Turn();
        }

    }


    // 재귀함수 이용
    void Think()
    {
        // Set Mvoe
        nextMove = Random.Range(-1, 2);

        // Move 애니메이션
        enemyAnim.SetInteger("WalkSpeed", nextMove);

        // 방향 Flip
        if(nextMove != 0)
        sprite.flipX = nextMove == 1;

        // Move 딜레이
        float nextThinkTime = Random.Range(2f, 6f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        sprite.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);
    }

    void Update()
    {

    }
}

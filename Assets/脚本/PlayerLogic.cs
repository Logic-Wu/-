using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLogic : MonoBehaviour
{
    //
    public float MoveSpeed = 1.0f;
    public float jumpForce = 1.0f;


    //
    private float InputX = 0;
    private Vector2 Movement;

    private bool isJump = false;

    private Rigidbody2D character;

    private Animator Animator;
    // Start is called before the first frame update
    void Start()
    {
        //角色控制器
        character = GetComponent<Rigidbody2D>();

        Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            character.AddForce(transform.up * jumpForce,ForceMode2D.Impulse);
            isJump = true;
        }
        if (character.velocity.y == 0)
        {
            isJump = false;
        }

        //动画
        if (InputX < 0)
        {
            Animator.SetBool("Idle", false);
            Animator.SetFloat("Direction", -1);
        }
            
        else if (InputX > 0)
        {
            Animator.SetBool("Idle", false);
            Animator.SetFloat("Direction", 1);
            
        }
            
        else
        {
            Animator.SetBool("Idle", true);
        }

        Movement = new Vector2(InputX * MoveSpeed, character.velocity.y);
        character.velocity = Movement;

    }
}

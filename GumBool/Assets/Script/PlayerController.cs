using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public string HAxisName = "Horizontal";
    public float JumpForce;
    public float HorizontalMaxSpeed;

    public bool enableMove;
    public bool Sliding = false;
    public bool IsGrounded { get { return isGrounded; } }
    bool isGrounded = true;

    Animator anim;
    SpriteRenderer spriteRend;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Sliding)
        {
            body.AddForce(new Vector2(0, -5));
            
        }
        if (enableMove)
        {
            float HVal = Input.GetAxis(HAxisName) * Speed;
            anim.SetFloat("speed", HVal);
            if (HVal < -0.1f)
            {
                spriteRend.flipX = true;
            }
            else if (HVal > 0.1f)
            {
                spriteRend.flipX = false;
            }

            float Hspeed;

            if (!isGrounded)
            {
                if (body.velocity.x <= HorizontalMaxSpeed && body.velocity.x >= -HorizontalMaxSpeed)
                {
                    Hspeed = body.velocity.x + HVal;
                    if (Hspeed > HorizontalMaxSpeed)
                    {
                        Hspeed = HorizontalMaxSpeed;
                    }
                    if (Hspeed < -HorizontalMaxSpeed)
                    {
                        Hspeed = -HorizontalMaxSpeed;
                    }
                }
                else
                {
                    Hspeed = body.velocity.x + HVal;
                    if (Hspeed > HorizontalMaxSpeed * 3)
                    {
                        Hspeed = HorizontalMaxSpeed * 3;
                    }
                    if (Hspeed < -HorizontalMaxSpeed * 3)
                    {
                        Hspeed = -HorizontalMaxSpeed * 3;
                    }
                }
            }
            else
            {
                Hspeed = HVal;
            }

            body.velocity = new Vector2(Hspeed, body.velocity.y);
            if (isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                anim.SetFloat("speed", 0);
                anim.SetTrigger("jump");
            }

            if(anim.GetBool("Jumping"))
            {
                if (body.velocity.y > 0)
                {
                    anim.speed = 0;
                    
                }
                else if (body.velocity.y <0)
                {
                    anim.speed = 1;
                }
            }
            Debug.Log(body.velocity.y);
        }

    }
    void Jump()
    {
        body.AddForce(new Vector2(0, JumpForce));
        anim.SetFloat("speed", 0);
        anim.SetTrigger("jump");

        isGrounded = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        anim.SetBool("Jumping", false);
        
    }

}

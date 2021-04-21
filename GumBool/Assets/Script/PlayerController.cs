using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public string HAxisName = "Horizontal";
    public float JumpForce;

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



        body.velocity = new Vector2(HVal, body.velocity.y);
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
                
        }

    }
    void Jump()
    {

        body.AddForce(new Vector2(0, JumpForce));
        anim.SetTrigger("jump");

        isGrounded = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

}

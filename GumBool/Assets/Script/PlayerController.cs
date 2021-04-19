using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1;
    public string HAxisName = "Horizontal";
    public float JumpForce;

    bool isGrounded = true;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        float HVal = Input.GetAxis(HAxisName) * Speed;

        body.velocity = new Vector2(HVal, body.velocity.y);
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }

    }
    void Jump()
    {

        body.AddForce(new Vector2(0, JumpForce));
        isGrounded = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

}

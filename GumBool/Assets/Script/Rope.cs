using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RopeSegment
{
    public Vector2 posNow;
    public Vector2 posOld;

    public RopeSegment(Vector2 pos)
    {
        this.posNow = pos;
        this.posOld = pos;
    }
}

public class Rope : MonoBehaviour
{
    float strenght;

    public float Mass;

    private LineRenderer lineRenderer;
    public List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public Vector3[] ropePositions;
    private float ropeSegLen = 0.3f;
    public int segmentLength;
    public GameObject Player;
    public float boxX, boxY;
    public Vector2 Velocity;
    public float JumpForce;
    bool EPressed;
    private float lineWidth = 0.1f;
    BoxCollider2D box;

    // Use this for initialization
    void Start()
    {
        JumpForce = 5f;
        this.lineRenderer = this.GetComponent<LineRenderer>();
        this.gameObject.AddComponent<BoxCollider2D>();
        boxX = 0.25f;
        boxY = 0.7f;
        box = this.GetComponent<BoxCollider2D>();
        box.size = new Vector2(boxX, boxY);
        box.isTrigger = true;
        EPressed = false;

        /*Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
        if (Input.GetKey(KeyCode.G))
        {
            strenght += 0.5f * Time.deltaTime;
            if (strenght >= 1.09f) //1.095 comincia già a farla girare tenendo premuto
            {
                strenght = 1.09f;
            }
        }
        else
        {
            strenght = 1f;
        }
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity * strenght;
            if(velocity.x == 0 && strenght > 0)
            {
                velocity.x += 10f;
                velocity.y += 10f;
            }
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
            if (i == this.segmentLength - 2)
            {
                box.offset = this.ropeSegments[i].posNow;
                Velocity = velocity;
            }
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        this.ropeSegments[0] = firstSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error * 0.5f;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        //ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            PlayerController controller = collision.GetComponent<PlayerController>();
            //if (!controller.IsGrounded)
            //{
                Animator anim = collision.GetComponent<Animator>();
                Rigidbody2D rigidbody = collision.GetComponent<Rigidbody2D>();
                if (Input.GetKey(KeyCode.E) && !EPressed)
                {
                    controller.enableMove = true;
                    rigidbody.gravityScale = 1;
                    //Debug.Log(Velocity.normalized * JumpForce);
                    Debug.Log(rigidbody.velocity);
                    //rigidbody.AddForce(Velocity.normalized * JumpForce);
                    rigidbody.velocity = new Vector2(Velocity.normalized.x * JumpForce, Velocity.normalized.y * JumpForce);
                    Debug.Log(rigidbody.velocity);

                    anim.SetFloat("speed", 0);
                    anim.SetTrigger("jump");
                    EPressed = true;
                }
                else if (!Input.GetKey(KeyCode.E))
                {
                    EPressed = false;
                    collision.transform.position = box.offset;
                    anim.SetFloat("speed", 1);
                    rigidbody.gravityScale = 0;
                    controller.enableMove = false;
                }
            //}
        }
        else if(collision.tag == "BlackInk")
        {
            Rigidbody2D rigidbody = collision.transform.parent.GetComponent<Rigidbody2D>();

            collision.transform.parent.position = box.offset;
            rigidbody.gravityScale = 0;
        }
        Debug.DrawLine(box.offset, box.offset + (Velocity.normalized * JumpForce), Color.yellow);
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderForce : MonoBehaviour
{
    private Rigidbody2D RB;
    public float BreakingSpeed;
    public float TotalVelo;

    public float BrickSubtraction;
    public float MetalSubtraction;

    private void Start()
    {
        RB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TotalVelo = Mathf.Abs(RB.velocity.x) + Mathf.Abs(RB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(TotalVelo >= BreakingSpeed && collision.gameObject.GetComponent<BreakingBoom>() != null)
        {
            GameObject Hit = collision.gameObject;
            
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
            Hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Hit.GetComponent<BreakingBoom>().ObjectBreak();
            //Hit.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if(collision.gameObject.GetComponent<Man>() != null)
        {
            GameObject Hit = collision.gameObject;

            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
            Hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Hit.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if(collision.gameObject.tag == "BrickWall")
        {
            PhyisicsApplication(BrickSubtraction);
        }

        if (collision.gameObject.tag == "MetalWall")
        {
            PhyisicsApplication(MetalSubtraction);
        }

        if (collision.gameObject.tag == "Spring")
        {
           
        }

    }

    void PhyisicsApplication(float SubtractionForce)
    {
        if (RB.velocity.x > 0)
        {
            if (Mathf.Abs(RB.velocity.x) > SubtractionForce)
            {
                RB.velocity -= new Vector2(SubtractionForce, 0);
            }

            if (Mathf.Abs(RB.velocity.x) <= SubtractionForce)
            {
                RB.velocity -= new Vector2(RB.velocity.x, 0);
            }
        }

        if (RB.velocity.x < 0)
        {
            if (Mathf.Abs(RB.velocity.x) > SubtractionForce)
            {
                RB.velocity += new Vector2(SubtractionForce, 0);
            }

            if (Mathf.Abs(RB.velocity.x) <= SubtractionForce)
            {
                RB.velocity += new Vector2(RB.velocity.x, 0);
            }
        }

        if (RB.velocity.y > 0)
        {
            if (Mathf.Abs(RB.velocity.y) > SubtractionForce)
            {
                RB.velocity -= new Vector2(0, SubtractionForce);
            }

            if (Mathf.Abs(RB.velocity.y) <= SubtractionForce)
            {
                RB.velocity -= new Vector2(0, RB.velocity.y);
            }
        }

        if (RB.velocity.y < 0)
        {
            if (Mathf.Abs(RB.velocity.y) > SubtractionForce)
            {
                RB.velocity += new Vector2(0, SubtractionForce);
            }

            if (Mathf.Abs(RB.velocity.y) <= SubtractionForce)
            {
                RB.velocity += new Vector2(0, RB.velocity.y);
            }
        }
    }

}

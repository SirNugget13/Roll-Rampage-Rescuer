using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spring : MonoBehaviour
{
    public float BounceMultiply;

    private void Start()
    {
        //Expand();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            float BounceForce = (float)(collision.gameObject.GetComponent<Rigidbody2D>().mass * BounceMultiply);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * BounceForce, ForceMode2D.Impulse);

            ExpandAndContract();
        }
    }

    private void ExpandAndContract()
    {
        LeanTween.scaleY(gameObject, 1.7f, 0.1f);
        this.Wait(0.1f, () =>
        {
            LeanTween.scaleY(gameObject, 1f, 0.7f);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float FishVelo;

    public bool MoveOnX = true;
    public bool MoveOnY = false;

    public GameObject LeftWall_BottomWall;
    public GameObject RightWall_TopWall;

    private void Start()
    {
        if (MoveOnX) { MoveOnY = false; }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(FishVelo, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == LeftWall_BottomWall)
        {
            if(MoveOnX)
            {
                Debug.Log("Yo");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(FishVelo, 0);
            }
            else
            {
                Debug.Log("Yo");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, FishVelo);
            }
            
        }

        if (collision.gameObject == RightWall_TopWall)
        {
            if (MoveOnX)
            {
                Debug.Log("Yo");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-FishVelo, 0);
            }
            else
            {
                Debug.Log("Yo");
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -FishVelo);
            }
        }
    }
}

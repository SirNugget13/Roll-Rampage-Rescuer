using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineSound : MonoBehaviour
{
    public AudioSource ASource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x >= 5 || collision.gameObject.GetComponent<Rigidbody2D>().velocity.y >= 5)
            {
                ASource.PlayOneShot(ASource.clip);
            }
        }
    }
}

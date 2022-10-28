using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    public bool IsDead = false;
    public float KillVelo = 20f;
    public AudioClip ExplodeSound;
    public AudioClip DeathSound;
    private Animator AnimController;
    public AudioSource ASource;

    private void Start()
    {
        AnimController = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (collision.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }

        if (collision.gameObject.tag == "Boulder")
        {
            BoulderForce Boulder = collision.gameObject.GetComponent<BoulderForce>();

            if (Boulder.TotalVelo > KillVelo)
            {
                PlayDeathSounds();
                StartCoroutine("KillDelay");
                IsDead = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                AnimController.Play("ManExplosion");
            }
        }

        else
        {
            if ((Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y)) * rb.mass > 100.0f)
            {
                PlayDeathSounds();
                IsDead = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                AnimController.Play("ManExplosion");
                StartCoroutine("KillDelay");
            }

            GameObject Hit = collision.gameObject;

            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<CapsuleCollider2D>());
            Hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Hit.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }
        
    public void PlayDeathSounds()
    {
        Debug.Log("Gabba");
        if(!IsDead)
        {
            Debug.Log("Goo");
            ASource.PlayOneShot(DeathSound);
            ASource.PlayOneShot(ExplodeSound);
        }
    }

        IEnumerator KillDelay()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    
}

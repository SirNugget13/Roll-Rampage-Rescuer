using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBoom : MonoBehaviour
{
    public Animator AnimController;
    public GameObject Explode;
    public string ExplosionName;
    public AudioClip ExplodeSound;
    public AudioSource ASource;

    // Start is called before the first frame update
    void Start()
    {
        AnimController = Explode.GetComponent<Animator>();
        ASource = gameObject.GetComponent<AudioSource>();
    }

    public void ObjectBreak()
    {
        AnimController.Play(ExplosionName, -1, 0f);
        ASource.PlayOneShot(ExplodeSound);
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        StartCoroutine("KillDelay");
    }

    IEnumerator KillDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

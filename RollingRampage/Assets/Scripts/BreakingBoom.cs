using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBoom : MonoBehaviour
{
    public Animator AnimController;
    public GameObject Explode;
    public string ExplosionName;

    // Start is called before the first frame update
    void Start()
    {
        AnimController = Explode.GetComponent<Animator>();
    }

    public void ObjectBreak()
    {
        AnimController.Play(ExplosionName, -1, 0f);
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        StartCoroutine("KillDelay");
    }

    IEnumerator KillDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}

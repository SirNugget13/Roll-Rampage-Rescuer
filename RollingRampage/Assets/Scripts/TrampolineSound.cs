using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineSound : MonoBehaviour
{
    public AudioSource ASource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ASource.PlayOneShot(ASource.clip);
    }
}

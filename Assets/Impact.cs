using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacts : MonoBehaviour
{
    public AudioSource impact;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Plane") {
            impact.Play();
        }
    }
}

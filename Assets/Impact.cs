using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacts : MonoBehaviour
{
    public AudioSource impact;
    public AudioSource gate;

    void OnCollisionEnter(Collision collision) 
    {
        string name = collision.collider.name;

        if (name == "Gate") {
            gate.Play();
        } else if (name != "Plane") {
            impact.Play();
        }
    }
}

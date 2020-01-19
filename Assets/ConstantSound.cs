using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTick : MonoBehaviour
{

    private AudioSource tick;

    // Start is called before the first frame update
    void Start()
    {
        tick = GetComponent<AudioSource>();
        tick.loop = true;
        tick.Play();
    }
}

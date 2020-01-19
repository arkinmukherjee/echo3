using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    AudioSource audioSource;

    bool play;

    // Start is called before the first frame update
    void Start()
    {
        play = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            RunRadar();
        }
    }

    public void RunRadar() {
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.LogWarning("Start Coroutine");
            // Invoke("playSound", hit.distance / 10);
            StartCoroutine("RadarSound", hit.distance / 10);
            // Invoke("playSound", ((int) hit.distance/10));
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.LogWarning("Did not Hit");
        }
    }

    private IEnumerator RadarSound(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Debug.LogWarning("About to play a sound!");
        audioSource.Play();
    }

    private void playSound(int dist) {
        audioSource.Stop();
        audioSource.Play();
        Debug.LogWarning("PLAYING SOUND");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource = (audioSources[0].clip.name == "water" ? audioSources[0] : audioSources[1]);
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

        int increment = 30;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        for(int i = 0; i <= 390; i+=increment) {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            Vector3 direction = Quaternion.AngleAxis(i, Vector3.up) * forward;
            Debug.LogWarning(Quaternion.AngleAxis(i, Vector3.up));

            Debug.DrawRay(transform.position, direction * 5, Color.green, 2);
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
            {
                // Debug.LogWarning("hit at " + (direction * hit.distance));
                StartCoroutine(RadarSoundLocation(transform.position + (direction * hit.distance), i/4f/increment));
            }
            else {
                Debug.DrawRay(transform.position, direction * 1000, Color.white);
                Debug.LogWarning("Did not Hit");
            }
        }
    }

    private IEnumerator RadarSoundLocation(Vector3 location, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        AudioSource.PlayClipAtPoint(audioSource.clip, location);
    }

    private IEnumerator RadarSound(float waitTime) {
        audioSource.Play();
        yield return new WaitForSeconds(waitTime);
        audioSource.Play();
    }

    private void playSound(int dist) {
        audioSource.Stop();
        audioSource.Play();
        Debug.LogWarning("PLAYING SOUND");
    }
}

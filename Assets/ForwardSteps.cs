using System.Collections;
using UnityEngine;

public class ForwardSteps : MonoBehaviour
{
    private Rigidbody body;

    private AudioSource footsteps;
    private bool playing;
    private bool toggle;

    // the minimum speed for which sound is made
    private float minSpeed = 0.01F;
 
    // Start is called before the first frame update
    void Start()
    {
        // get the rigid body.
        body = GetComponent<Rigidbody>();

        // initialize the sound.
        AudioSource[] sources = GetComponents<AudioSource>();
        footsteps = (sources[0].clip.name == "Footsteps" ? sources[0] : sources[1]);
        playing = false;
        toggle = false;
        footsteps.Play();
        footsteps.Pause();
    }

    // Update is called once per frame
    void Update()
    {

        // if we just set the toggle, either play or stop according to "playing" status.
        // the "looping" checkbox in the AudioSource takes care of looping.
        if (playing && toggle) {
            footsteps.UnPause();
            toggle = false;
        } else if (!playing && toggle) {
            footsteps.Pause();
            toggle = false;
        }

        // at this point toggle will always be false.

        // true if body is moving forward.
        bool forwards = body.velocity.magnitude > minSpeed;
        Debug.Log(body.velocity.magnitude);
        
        if (!playing && forwards) {
            // if we aren't playing, and the keys are down, then play.
            playing = true;
            toggle = true;
        } else if (playing && !forwards) {
            // if we are playing and the keys are not down, then stop playing.
            playing = false;
            toggle = true;
        }
        // else: not playing, not moving. OK.
        // else: playing, moving. OK.
    }
}

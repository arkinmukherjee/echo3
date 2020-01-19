using System.Collections;
using UnityEngine;

public class MazeSounds : MonoBehaviour
{
    public Rigidbody body;

    public AudioSource footsteps;
    public AudioSource impact;
    public AudioSource gate;
    public AudioSource scrape;

    private bool stepsPlaying;
    private bool stepsToggle;

    private bool scrapePlaying;
    bool touchingWall;
    bool scrapeToggle;

    // the minimum speed for which sound is made
    private float minSpeed = 5.3F;
 
    // Start is called before the first frame update
    void Start() {

        // don't play on start.
        impact.playOnAwake = false;
        gate.playOnAwake = false;
        scrape.playOnAwake = false;
        footsteps.playOnAwake = false;

        // scrape and footsteps should loop, impact and gate should not.
        scrape.loop = true;
        impact.loop = false;
        gate.loop = false;
        footsteps.loop = true;

        // start scrape, but pause it. same with footsteps.
        scrape.Play();
        scrape.Pause();
        footsteps.Play();
        footsteps.Pause();

        // keep track of foosteps toggle.
        stepsPlaying = false;
        stepsToggle = false;

        // keep track of scrape toggle.
        scrapePlaying = false;
        
        // initially not touching wall.
        touchingWall = false;
        scrapeToggle = false;
    }

    // Update is called once per frame
    void Update() {

        // if we just set the toggle, either play or stop according to "stepsPlaying" status.
        // the "looping" checkbox in the AudioSource takes care of looping.
        if (stepsPlaying && stepsToggle) {
            footsteps.UnPause();
            stepsToggle = false;
        } else if (!stepsPlaying && stepsToggle) {
            footsteps.Pause();
            stepsToggle = false;
        }

        // at this point toggle will always be false.

        // true if body is moving forward.
        bool forwards = body.velocity.magnitude > minSpeed;
        
        if (!stepsPlaying && forwards) {
            // if we aren't playing, and the keys are down, then play.
            stepsPlaying = true;
            stepsToggle = true;
        } else if (stepsPlaying && !forwards) {
            // if we are playing and the keys are not down, then stop playing.
            stepsPlaying = false;
            stepsToggle = true;
        }
        // else: not playing, not moving. OK.
        // else: playing, moving. OK.

        if (scrapePlaying && scrapeToggle) {
            scrape.UnPause();
            scrapeToggle = false;
        } else if (!scrapePlaying && scrapeToggle) {
            scrape.Pause();
            scrapeToggle = false;
        }
        
        if (touchingWall && scrapePlaying && !forwards) {
            // if (touching wall), scrape is playing, and we've stopped moving, then pause.
            scrapePlaying = false;
            scrapeToggle = true;
        } else if (touchingWall && !scrapePlaying && forwards) {
            // if (touching wall), scrape not playing, and we're moving, then play.
            scrapePlaying = true;
            scrapeToggle = true;
        } else if (!touchingWall) {
            // else: not touching wall. stop.
            scrapePlaying = false;
            scrapeToggle = true;
        }
    }

    // record when the collision starts
    void OnCollisionEnter(Collision collision) {

        string name = collision.collider.name;

        if (name == "Gate") {
            gate.Play();
        } else if (name != "Plane") {
            impact.Play();
            touchingWall = true;
        }
    }

    // record when the collision ends
    void OnCollisionExit(Collision collision) {

        string name = collision.collider.name;

        if (name != "Gate" && name != "Plane") {
            touchingWall = false;
        }
    }
}

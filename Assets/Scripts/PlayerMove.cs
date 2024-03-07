using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float movementspeed = 2f;
    private float ogspeed;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float dodgetimer = 0f;
    private bool spacepressed = false;
    private bool dodging = false;
    private float dodgingtime = 0f;
    [SerializeField] private float rollspeed = 30f;
    [SerializeField] private float dodgingmaxtime = 0.25f;
    [SerializeField] private float dodgetimermax = 1f;
    private AudioSource walksound;
    private bool walking;
    //[SerializeField] private float walksoundcutoff = 1f;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ogspeed = movementspeed;
        walksound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(0, 3, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dodging)
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if (direction.x > 0) {
            // flip horizontally right
            flipSprite(true);
        }
        else if (direction.x < 0) {
            // flip left aka revert to original
            flipSprite(false);
        }
        
        if (dodgetimer > 0)
        {
            dodgetimer -= Time.deltaTime;
        }
        if(dodgingtime > 0)
        {
            dodgingtime -= Time.deltaTime;
        }
        if(dodgingtime <= 0)
        {
            dodging = false;
        }

        if(walking && !walksound.isPlaying)
        {
            walksound.Play();
        }
    }

    private void FixedUpdate()
    {
        if (dodging)
        {
            movementspeed = rollspeed;
        }
        else
        {
            movementspeed = ogspeed;

            if (Input.GetKey(KeyCode.Space))
            {
                if (!spacepressed && dodgetimer <= 0)
                {
                    //rb.AddForce(direction * rollspeed);
                    //movementspeed = rollspeed;
                    dodging = true;
                    dodgetimer = dodgetimermax;
                    dodgingtime = dodgingmaxtime;
                }
                spacepressed = true;
            }
            else
            {
                spacepressed = false;
            }
        }

        rb.AddForce(direction * movementspeed);


        if (rb.velocity.sqrMagnitude != 0)
        {
            if (!walking) {
                animator.Play("Base Layer.walkingCycle");
            }
            walking = true;
        }
        else
        {
            walking = false;
            animator.Play("Base Layer.Idle");
            
        }
    }

    private void flipSprite(bool left) {
        // left is the default that I drew it to face

        // Get the current scale of the sprite
        Vector3 scale = transform.localScale;

        // If flip is true, flip the sprite horizontally; otherwise, restore its original scale
        scale.x = left ? -1 * Mathf.Abs(scale.x) : Mathf.Abs(scale.x);

        // Apply the new scale to the sprite
        transform.localScale = scale;
    }
}

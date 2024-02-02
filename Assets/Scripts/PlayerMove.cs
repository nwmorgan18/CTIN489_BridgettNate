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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ogspeed = movementspeed;
        walksound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dodging)
        {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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

        rb.velocity = direction * movementspeed;
       

        if (rb.velocity.sqrMagnitude != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
        /*
        if (rb.velocity.magnitude <= walksoundcutoff || rb.velocity.magnitude >= walksoundcutoff * -1)
        {
            walking = false;
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardMove : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float chargetime = 0.5f;
    [SerializeField] private float lungetime = 3f;
    [SerializeField] private float aimtime = 2f;
    [SerializeField] private float speed = 10f;
    private bool aiming = true;
    private bool charging = false;
    private bool lunging = false;
    private Vector2 lungedir;
    private Rigidbody2D rb;


    private float currenttime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currenttime = aimtime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currenttime > 0)
        {
            currenttime -= Time.deltaTime;
        }

        if (aiming)
        {
            //continuously look at player, don't move
            lungedir = player.transform.position - transform.position;
            lungedir.Normalize();

            transform.LookAt(player.transform.position, new Vector3(0,0,1));
            
            if (currenttime <= 0)
            {
                aiming = false;
                currenttime = chargetime;
                charging = true;
            }
        }
        else if (charging)
        {
            //dont rotate dont move
            if(currenttime <= 0)
            {
                charging = false;
                currenttime = lungetime;
                lunging = true;
            }

        }
        else if (lunging)
        {
            //dont rotate, move in direction of 
            rb.AddForce(transform.forward * speed);

            if(currenttime <= 0)
            {
                lunging = false;
                currenttime = aimtime;
                aiming = true;
            }
        }

        //Vector3 temprot = transform.rotation;

    }
}

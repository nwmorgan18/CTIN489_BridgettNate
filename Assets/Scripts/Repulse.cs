using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Repulse : MonoBehaviour
{
    [SerializeField] private float repulsespeed = 200f;
    Rigidbody2D rb;
    GameObject helper;
    Vector2 capsulepos;
    Vector2 helperpos;
    Vector2 direction;
    //Vector2 gotopos;
    float distancemulti;
    [SerializeField] private float orbitspeed = 10f;
    //[SerializeField] private float maxspeed = 100f;
    //bool mousedown = false;

    //GameObject mainCamera;
    //private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //mainCamera = GameObject.FindWithTag("MainCamera");
        helper = GameObject.FindWithTag("Helper");
    }

    private void Update()
    {
        
    }

        // Update is called once per frame
    void FixedUpdate()
    {
        
        /*
        if(rb.velocity.magnitude > maxspeed)
        {
            Vector2 olddirection = rb.velocity.normalized;
            rb.velocity = olddirection * maxspeed;
        }
        */

        if (Input.GetMouseButton(0))
        {
            //mousedown = true;
            /*
            helperpos = helper.transform.position;
            gotopos = Vector2.Lerp(transform.position, helperpos, orbitspeed);
            rb.MovePosition(gotopos);
            */

            capsulepos = (Vector2)transform.position;
            helperpos = (Vector2)helper.transform.position;
            direction = helperpos - capsulepos;
            //rb.velocity = direction.normalized * orbitspeed;
            rb.AddForce(direction.normalized * orbitspeed);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            //mousedown = false;
            capsulepos = (Vector2)transform.position;
            helperpos = (Vector2)helper.transform.position;
            direction = capsulepos - helperpos;
            distancemulti = 1 / (capsulepos - helperpos).magnitude;
            rb.AddForce(direction.normalized * repulsespeed * distancemulti);
            // mainCamera.GetComponent<ShakeBehavior>().TriggerShake();
        }
        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}

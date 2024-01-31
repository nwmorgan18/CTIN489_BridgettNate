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
    Vector2 playerpos;
    Vector2 direction;
    float distancemulti;
    [SerializeField] private float orbitspeed = 10f;
    [SerializeField] private float maxspeed = 100f;
    bool mousedown = false;

    //GameObject mainCamera;
    //private float angle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //mainCamera = GameObject.FindWithTag("MainCamera");
        helper = GameObject.FindWithTag("Helper");
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > maxspeed)
        {
            Vector2 olddirection = rb.velocity.normalized;
            rb.velocity = olddirection * maxspeed;
        }

        if (Input.GetMouseButtonDown(0) || mousedown)
        {
            capsulepos = (Vector2)transform.position;
            playerpos = (Vector2)helper.transform.position;
            direction = playerpos - capsulepos;
            float movementX = direction.x;
            float movementY = direction.y;
            Vector3 movement = new Vector3(movementX, movementY, 0f);
            rb.AddForce(movement.normalized * orbitspeed);
            mousedown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousedown = false;
            capsulepos = (Vector2)transform.position;
            playerpos = (Vector2)helper.transform.position;
            direction = capsulepos - playerpos;
            distancemulti = 1 / (capsulepos - playerpos).magnitude;
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

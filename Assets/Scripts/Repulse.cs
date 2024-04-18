using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Repulse : MonoBehaviour
{
    [SerializeField] private float repulsespeed = 200f;
    Rigidbody2D rb;
    GameObject helper;
    Vector2 capsulepos;
    Vector2 helperpos;
    Vector2 direction;
    [SerializeField] private int capsulenum;
    private List<Vector2> capsuleslots;
    Collider2D coll;
    [SerializeField] GameObject LightningStart;
    [SerializeField] GameObject LightningEnd;
    //GameObject lightningclone;
    GameObject currentenemy;
    [SerializeField] float LightningLifeTime = 0.5f;
    float lightningtime = 0.0f;
    Vector2 dumbystart;
    Vector2 dumbyend;
    private AudioSource shocksound;

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
        coll = GetComponent<Collider2D>();
        //mainCamera = GameObject.FindWithTag("MainCamera");
        helper = GameObject.FindWithTag("Helper");
        capsuleslots = new List<Vector2>();
        capsuleslots.Add(new Vector2(0, -2));
        capsuleslots.Add(new Vector2(0, 2));
        capsuleslots.Add(new Vector2(2, 0));
        capsuleslots.Add(new Vector2(-2, 0));
        capsuleslots.Add(new Vector2(1.5f, 1.5f));
        capsuleslots.Add(new Vector2(-1.5f, -1.5f));
        capsuleslots.Add(new Vector2(-1.5f, 1.5f));
        capsuleslots.Add(new Vector2(1.5f, -1.5f));

        dumbystart = new Vector2(100, 100);
        dumbyend = new Vector2(100, 101);

        shocksound = GetComponent<AudioSource>();

        //lightningclone = null;
        Physics2D.IgnoreCollision(GameObject.FindWithTag("Player").GetComponent<Collider2D>(), coll);
    }

    private void Update()
    {
        
        
        if (lightningtime > 0)
        {
            lightningtime -= Time.deltaTime;

            //Lightning.transform.position = this.transform.position;

            if (currentenemy != null)
            {
                LightningStart.transform.position = this.transform.position;
                LightningEnd.transform.position = currentenemy.transform.position;
            }
            else
            {
                LightningStart.transform.position = dumbystart;
                LightningEnd.transform.position = dumbyend;
            }
            
        }
        else
        {
            if (shocksound.isPlaying)
            {
                shocksound.Pause();
            }
            LightningStart.transform.position = dumbystart;
            LightningEnd.transform.position = dumbyend;
        }
        

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
        else if (Input.GetMouseButton(0))
        {
            //mousedown = true;
            /*
            helperpos = helper.transform.position;
            gotopos = Vector2.Lerp(transform.position, helperpos, orbitspeed);
            rb.MovePosition(gotopos);
            */

            capsulepos = (Vector2)transform.position;
            helperpos = (Vector2)helper.transform.position;
            direction = helperpos - capsulepos + capsuleslots[capsulenum];
            //rb.velocity = direction.normalized * orbitspeed;
            rb.AddForce(direction.normalized * orbitspeed);

        }

        if (Input.GetKeyUp("r"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if (Input.GetKeyUp("m"))
        {
            SceneManager.LoadScene("Start");
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            shocksound.Play();
            currentenemy = other.gameObject;
            //lightningclone = Instantiate(Lightning, this.transform);
            lightningtime = LightningLifeTime;
            //lightningclone.GetComponent<LightningBoltScript>().StartPosition = this.transform.position;
        }
    }
}

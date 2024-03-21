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
    private AudioSource damagesound;
    [SerializeField] private int health = 3;
    [SerializeField] private float invincibletime = 1f;
    private float currentlyinvincible = 0f;
    [SerializeField] GameObject shippiece;


    private float currenttime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currenttime = aimtime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Capsule") && currentlyinvincible <= 0f)
        {
            //Debug.Log("Lizard Hit");
            health -= 1;
            currentlyinvincible = invincibletime;
            //damagesound.Play();
            if (health <= 0)
            {
                /*
                spawner.GetComponent<ShipPieceSpawn>().AddKill(1);
                if (spawner.GetComponent<ShipPieceSpawn>().GetKills() >= spawner.GetComponent<ShipPieceSpawn>().GetNeededKills())
                {
                    spawner.GetComponent<ShipPieceSpawn>().SetPieceLocation(this.transform.position);
                }
                */
                //Debug.Log("Lizard Die");
                shippiece.GetComponent<Level2ShipPieceControl>().KillLizard();
                Destroy(this.gameObject);
            }
        }
    }
        

        // Update is called once per frame
        void Update()
    {
        if (currentlyinvincible > 0)
        {
            currentlyinvincible -= Time.deltaTime;
        }

        if (currenttime > 0)
        {
            currenttime -= Time.deltaTime;
        }

        if (aiming)
        {
            //continuously look at player, don't move
            lungedir = player.transform.position - transform.position;
            lungedir.Normalize();

            transform.right = lungedir;

            //transform.LookAt(player.transform.position, new Vector3(0,0,1));

            if (currenttime <= 0)
            {
                aiming = false;
                currenttime = chargetime;
                rb.velocity = Vector2.zero;
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
            transform.right = lungedir;

            rb.AddForce(transform.right * speed);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMove : MonoBehaviour
{
    //private Vector3 playerPosition;
    private GameObject player;
    //[SerializeField] private float moveSpeed = 3.5f;
    Rigidbody2D rb;
    //Vector2 position = new Vector2(0f, 0f);
    Vector2 direction;
    private AudioSource damagesound;
    [SerializeField] private int health = 3;
    [SerializeField] private float invincibletime = 1f;
    private int currhealth;
    private float currentlyinvincible = 0f;
    private GameObject spawner;
    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float colorDuration = 0.5f;
    private float timer = 0f;
    private bool justHit;
    public int targetSceneBuildIndex; // Name of the scene to transition to
    [SerializeField] private GameObject shippiece;
    [SerializeField] private Slider healthbar;
    [SerializeField] private GameObject bulletprefab;
    private List<Vector2> bulletlocations;
    [SerializeField] private float shoottime = 10f;
    private float currshoot;
    private int randnum = -1;
    [SerializeField] private float shootpause = 1f;
    private float currentpause = 0f;
    private int radialindex;
    bool runningradial = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        damagesound = GetComponent<AudioSource>();
        spawner = GameObject.FindWithTag("Spawner");
        //agent = GetComponent <NavMeshAgent>();
        animator = GetComponent<Animator>();
        // start walking animation
        // animator.Play("Base Layer.Enemy1Walk");
        // sprite renderer for color change
        spriteRenderer = GetComponent<SpriteRenderer>();
        justHit = false;
        currhealth = health;

        bulletlocations = new List<Vector2>() { new Vector2(0f, -12f), new Vector2(-4.24f, -10.24f), new Vector2(-6f, -6f), new Vector2(-4.24f, -1.76f), new Vector2(0f, 0f), new Vector2(4.24f, -1.76f), new Vector2(6f, -6f), new Vector2(4.24f, -10.24f) };

        currshoot = shoottime;
        
        //GameObject bullet = Instantiate(bulletprefab, this.transform.position, Quaternion.identity);
        //bullet.GetComponent<BulletMove>().SetDirection(new Vector2(0f, -1f));
    }

    // Update is called once per frame
    void Update()
    {
        if (justHit) {
            changeColor();
        }
        //agent.SetDestination(player.transform.position);
        if(currentlyinvincible > 0)
        {
            currentlyinvincible -= Time.deltaTime;
            //agent.speed = 0f;
        }
        
        if(currentpause > 0)
        {
            currentpause -= Time.deltaTime;
        }
        else if(currshoot > 0)
        {
            currshoot -= Time.deltaTime; 
        }

        if (currentpause <=0 && runningradial)
        {
            GameObject bullet = Instantiate(bulletprefab, bulletlocations[radialindex], Quaternion.identity);
            radialindex++;
            currentpause = shootpause;

            if(radialindex >= bulletlocations.Count)
            {
                currentpause = 0f;
                runningradial = false;
                radialindex = 0;
            }
        }

        if(currshoot <= 0)
        {
            randnum = Random.Range(0, 2);
            //randnum = 1;

            if(randnum == 0)
            {
                for (int i = 0; i < bulletlocations.Count; i += 2)
                {
                    GameObject bullet = Instantiate(bulletprefab, bulletlocations[i], Quaternion.identity);
                    //bullet.GetComponent<BulletMove>().SetDirection(new Vector2(bullet.transform.position.x - transform.position.x, bullet.transform.position.y - transform.position.y - 12f));
                }
            }

            if(randnum == 1)
            {
                GameObject bullet = Instantiate(bulletprefab, bulletlocations[radialindex], Quaternion.identity);
                radialindex++;
                currentpause = shootpause;
                runningradial = true;
            }

            if(randnum == 2)
            {
                for (int i = 1; i < bulletlocations.Count; i += 2)
                {
                    GameObject bullet = Instantiate(bulletprefab, bulletlocations[i], Quaternion.identity);
                    //bullet.GetComponent<BulletMove>().SetDirection(new Vector2(bullet.transform.position.x - transform.position.x, bullet.transform.position.y - transform.position.y - 12f));
                }
            }


            currshoot = shoottime;
        }
    }
    private void FixedUpdate()
    {
        /*
        direction = player.transform.position - this.transform.position;
        //rb.MovePosition(position);
        rb.velocity = direction.normalized * moveSpeed;

        if (direction.x > 0)
        {
            // flip horizontally right
            flipSprite(false);
        }
        else if (direction.x < 0)
        {
            // flip left aka revert to original
            flipSprite(true);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.CompareTag("Player"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        */

        if(other.gameObject.CompareTag("Capsule") && currentlyinvincible <= 0f)
        {
            justHit = true;
            changeColor();
            currhealth -= 1;
            healthbar.value = (float)currhealth / (float)health;
            //Debug.Log("Enemy Hit");
            currentlyinvincible = invincibletime;
            //damagesound.Play();
            if(currhealth <= 0)
            {
                /*
                spawner.GetComponent<ShipPieceSpawn>().AddKill(1);
                if (spawner.GetComponent<ShipPieceSpawn>().GetKills() >= spawner.GetComponent<ShipPieceSpawn>().GetNeededKills())
                {
                    spawner.GetComponent<ShipPieceSpawn>().SetPieceLocation(this.transform.position);
                }
                */
                shippiece.GetComponent<Level3ShipPieceSpawn>().KillBoss();
                Destroy(this.gameObject);
                //SceneManager.LoadScene(targetSceneBuildIndex); // Load the specified scene

            }
        }
        // for animation attack
        if (other.gameObject.CompareTag("Player")) 
        {
            // animator.Play("Base Layer.Enemy1Bite");
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

    private void changeColor() {
        spriteRenderer.color = Color.red;

        timer += Time.deltaTime;

        if (timer >= colorDuration) {
            spriteRenderer.color = Color.white;
            timer = 0f;
            justHit = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    //private Vector3 playerPosition;
    private GameObject player;
    [SerializeField] private float moveSpeed = 3.5f;
    Rigidbody2D rb;
    //Vector2 position = new Vector2(0f, 0f);
    Vector2 direction;
    private AudioSource damagesound;
    [SerializeField] private int health = 3;
    [SerializeField] private float invincibletime = 1f;
    private float currentlyinvincible = 0f;
    private GameObject spawner;
    //private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float colorDuration = 0.5f;
    private float timer = 0f;
    private bool justHit;
    bool isDead;
    bool islevel3 = false;
    Collider2D coll;


    // Start is called before the first frame update
    void Start()
    {
        string currentscene = SceneManager.GetActiveScene().name;
        if(currentscene == "Level 3 (Boss)")
        {
            islevel3 = true;
        }
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        damagesound = GetComponent<AudioSource>();
        spawner = GameObject.FindWithTag("Spawner");
        //agent = GetComponent <NavMeshAgent>();
        animator = GetComponent<Animator>();
        // start walking animation
        animator.Play("Base Layer.Enemy1Walk");
        // sprite renderer for color change
        spriteRenderer = GetComponent<SpriteRenderer>();
        justHit = false;
        isDead = false;


        coll = GetComponent<Collider2D>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Bullet");
        //GameObject obj = GameObject.FindWithTags(allignoretags[i]);
        for (int j = 0; j < objs.Length; j++)
        {
            if (objs[j] != null)
            {
                Physics2D.IgnoreCollision(objs[j].GetComponent<Collider2D>(), coll);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
        if(currentlyinvincible > 0)
        {
            currentlyinvincible -= Time.deltaTime;
            //agent.speed = 0f;
        }
        else
        {
            //agent.speed = moveSpeed;
        }
    }
    private void FixedUpdate()
    {
        if (justHit) {
            changeColor();
        }
        
        if(currentlyinvincible <= 0)
        {
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
        }
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
            health -= 1;
            //Debug.Log("Enemy Hit");
            currentlyinvincible = invincibletime;
            damagesound.Play();
            changeColor();
            justHit = true;
            if(health <= 0)
            {
                Debug.Log("Wolf Die");
                Die();
            }
        }
        // for animation attack
        if (other.gameObject.CompareTag("Player")) 
        {
            animator.Play("Base Layer.Enemy1Bite");
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

    private IEnumerator DieCoroutine() {
        
        // Play death animation
        animator.Play("Base Layer.WolfDie");

        // Wait for the length of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // old stuff
        if (!islevel3)
        {
            spawner.GetComponent<ShipPieceSpawn>().AddKill(1);
            if (spawner.GetComponent<ShipPieceSpawn>().GetKills() >= spawner.GetComponent<ShipPieceSpawn>().GetNeededKills())
            {
                //spawner.GetComponent<ShipPieceSpawn>().SetPieceLocation(this.transform.position);
            }
            spawner.GetComponent<EnemySpawn>().KillFirst();
        }

        Destroy(this.gameObject);

        isDead = false;

    }

    private void Die() {
        if (isDead) {
            return;
        }
        isDead = true;

        StartCoroutine(DieCoroutine());
    }
}


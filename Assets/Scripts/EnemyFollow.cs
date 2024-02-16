using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    //private Vector3 playerPosition;
    private GameObject player;
    [SerializeField] private float moveSpeed = 0.1f;
    Rigidbody2D rb;
    //Vector2 position = new Vector2(0f, 0f);
    Vector2 direction;
    private AudioSource damagesound;
    [SerializeField] private int health = 3;
    [SerializeField] private float invincibletime = 1f;
    private float currentlyinvincible = 0f;
    private GameObject spawner;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        damagesound = GetComponent<AudioSource>();
        spawner = GameObject.FindWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyinvincible > 0)
        {
            currentlyinvincible -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
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
        if (other.gameObject.CompareTag("Player"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
        if(other.gameObject.CompareTag("Capsule") && currentlyinvincible <= 0f && !Input.GetMouseButton(0))
        {
            health -= 1;
            Debug.Log("Enemy Hit");
            currentlyinvincible = invincibletime;
            damagesound.Play();
            if(health <= 0)
            {
                spawner.GetComponent<ShipPieceSpawn>().AddKill(1);
                Destroy(this.gameObject);
            }
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

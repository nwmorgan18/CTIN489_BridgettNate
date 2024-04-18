using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxhealth = 3;
    private int curhealth;
    private Rigidbody2D rb;
    [SerializeField] float pushback = 100f;
    [SerializeField] float invicibletime = 1f;
    private float curtime = 0f;
    [SerializeField] float shakeintensity = 5f;
    [SerializeField] float shaketime = 0.1f;
    [SerializeField] GameObject TelemetryManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float colorDuration = 0.5f;
    private float timer = 0f;
    private bool justHit;
    private bool isDead;

    // UI stuff
    public Sprite fullHealthSprite;
    public Sprite medHealthSprite;
    public Sprite lowHealthSprite;
    public UnityEngine.UI.Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        curhealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // sprite renderer for color change
        spriteRenderer = GetComponent<SpriteRenderer>();
        justHit = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (justHit) {
            changeColor();
        }
        // update UI
        UpdateHealthUI();

        if (curhealth <= 0)
        {
            //TelemetryManager.GetComponent<PlayerMetricRecord>().PlayerDied();

            Die();

        }
        if (curtime > 0)
        {
            curtime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDead) {
            return;
        }
        if (other.gameObject.CompareTag("Enemy") && curtime <= 0f)
        {
            justHit = true;
            changeColor();
            curhealth--;
            Vector2 direction = transform.position - other.gameObject.transform.position;
            rb.AddForce(direction.normalized * pushback);
            curtime = invicibletime;
            CameraShake.Instance.Shake(shakeintensity, shaketime);
            Debug.Log("Player Hit");
            // animator.Play("Hurt.AstronautHurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) {
            return;
        }
        if (other.gameObject.CompareTag("Poison"))
        {
            curhealth = maxhealth;
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
            curhealth = maxhealth;
            Debug.Log("Swamped");
        }
    }

    private void UpdateHealthUI() {
        if (isDead) {
            return;
        }
        if (curhealth == 3) {
            healthImage.sprite = fullHealthSprite;
        }
        else if (curhealth == 2) {
            healthImage.sprite = medHealthSprite;
        }
        else if (curhealth == 1) {
            healthImage.sprite = lowHealthSprite;
        }
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
    animator.Play("Base Layer.AstronautDie");

    // Wait for the length of the death animation
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

    // Restart the scene
    isDead = false;
    curhealth = maxhealth;
    string currentscene = SceneManager.GetActiveScene().name;
    SceneManager.LoadScene(currentscene);
    curhealth = maxhealth;
}

    private void Die() {
        if (isDead) {
            return;
        }
        isDead = true;
        this.gameObject.GetComponent<PlayerMove>().Die();

        StartCoroutine(DieCoroutine());
    }
}

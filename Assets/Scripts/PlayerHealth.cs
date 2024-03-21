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


    // UI stuff
    public Sprite fullHealthSprite;
    public Sprite medHealthSprite;
    public Sprite lowHealthSprite;
    public UnityEngine.UI.Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        curhealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // update UI
        UpdateHealthUI();

        if (curhealth <= 0)
        {
            //TelemetryManager.GetComponent<PlayerMetricRecord>().PlayerDied();

            curhealth = maxhealth;
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
            curhealth = maxhealth;
        }
        if (curtime > 0)
        {
            curtime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && curtime <= 0f)
        {
            curhealth--;
            Vector2 direction = transform.position - other.gameObject.transform.position;
            rb.AddForce(direction.normalized * pushback);
            curtime = invicibletime;
            CameraShake.Instance.Shake(shakeintensity, shaketime);
            Debug.Log("Player Hit");
            animator.Play("Base Layer.AstronautHurt");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
}

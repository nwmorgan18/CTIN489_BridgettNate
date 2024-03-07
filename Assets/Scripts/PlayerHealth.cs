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

    // Start is called before the first frame update
    void Start()
    {
        curhealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (curhealth <= 0)
        {
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
        }
    }
}

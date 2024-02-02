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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //playerPosition = player.transform.position;
        //playerPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //position = Vector2.Lerp(transform.position, playerPosition, moveSpeed);
    }
    private void FixedUpdate()
    {
        direction = player.transform.position - this.transform.position;
        //rb.MovePosition(position);
        rb.velocity = direction.normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string currentscene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentscene);
        }
    }
}

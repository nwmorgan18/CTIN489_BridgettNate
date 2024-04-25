using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private float waittime = 1.5f;
    [SerializeField] private float speed = 100f;
    private float currenttime;
    private Rigidbody2D rb;
    private GameObject boss;


    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        direction.Normalize();
    }

    // Start is called before the first frame update
    void Start()
    {
        currenttime = waittime;
        rb = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        direction = transform.position - boss.transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        currenttime -= Time.deltaTime;

        if(currenttime <= 0)
        {
            rb.AddForce(direction * speed * Time.deltaTime);
        }
    }
}

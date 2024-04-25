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
    [SerializeField] private float lifespan = 30f;
    private float lifetime;

    List<string> allignoretags;
    Collider2D coll;


    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        direction.Normalize();

    }

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        allignoretags = new List<string>() { "Tree", "Enemy", "Wall", "PlayerFeet", "Capsule" };

        currenttime = waittime;
        rb = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        //direction = new Vector2(transform.position.x-boss.transform.position.x, transform.position.y - boss.transform.position.y - 6f);
        Vector3 startpos = new Vector3(0f, -6f, 0f);
        direction = transform.position - startpos;
        direction.Normalize();
        lifetime = lifespan;

        for (int i = 0; i < allignoretags.Count; i++)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(allignoretags[i]);
            //GameObject obj = GameObject.FindWithTags(allignoretags[i]);
            for (int j = 0; j < objs.Length; j++)
            {
                if (objs[j] != null)
                {
                    Physics2D.IgnoreCollision(objs[j].GetComponent<Collider2D>(), coll);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {


        currenttime -= Time.deltaTime;

        if(currenttime <= 0)
        {
            rb.AddForce(direction * speed * Time.deltaTime);
        }

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(this);
        }
    }
}

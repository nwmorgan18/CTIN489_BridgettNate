using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperIgnore : MonoBehaviour
{
    List<string> allignoretags;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();

        allignoretags = new List<string>(){"Player", "Enemy", "Wall", "Tree"};
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allignoretags.Count; i++)
        {
            GameObject obj = GameObject.FindWithTag(allignoretags[i]);
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), coll);
        }
    }
}

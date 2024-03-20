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

        allignoretags = new List<string>(){ "Tree", "Player", "Enemy", "Wall", "PlayerFeet"};
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allignoretags.Count; i++)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(allignoretags[i]);
            //GameObject obj = GameObject.FindWithTags(allignoretags[i]);
            for(int j = 0; j < objs.Length; j++)
            {
                if(objs[j] != null)
                {
                    Physics2D.IgnoreCollision(objs[j].GetComponent<Collider2D>(), coll);
                }
            }

        }
    }
}

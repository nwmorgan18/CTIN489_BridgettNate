using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    List<Vector2> spawnlocations;
    public GameObject EnemyPrefab;
    [SerializeField] private float spawntime = 30f;
    private float currentwait = 0f;
    [SerializeField] private float spawndecay = 2f;
    [SerializeField] private float minspawntime = 3f;
    [SerializeField] GameObject helper;
    private AudioSource spawnsound;
    bool pieceacquired = false;

   
    // Start is called before the first frame update
    void Start()
    {
        spawnlocations = new List<Vector2>() { new Vector2(-38f, -11.5f), new Vector2(-41f, 17f), new Vector2(41.9f, 16.7f), new Vector2(37.6f, -10.6f) };
        currentwait = spawntime;
        spawnsound = GetComponent<AudioSource>();        
    }

    public void AcquirePiece()
    {
        pieceacquired = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pieceacquired)
        {
            if (currentwait > 0)
            {
                currentwait -= Time.deltaTime;
            }

            if (currentwait <= 0)
            {
                Debug.Log("Spawn Enemy");
                int randnum = Random.Range(0, spawnlocations.Count - 1);
                transform.position = spawnlocations[randnum];
                GameObject newenemy = Instantiate(EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                
                Physics2D.IgnoreCollision(helper.GetComponent<Collider2D>(), newenemy.GetComponent<Collider2D>());
                spawnsound.Play();
                if (spawntime >= 10)
                {
                    spawntime -= spawndecay;
                    if(spawntime < minspawntime)
                    {
                        spawntime = minspawntime;
                    }
                }
                currentwait = spawntime;
            }
        }
    }
}

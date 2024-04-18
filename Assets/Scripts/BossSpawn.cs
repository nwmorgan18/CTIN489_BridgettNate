using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    List<Vector2> spawnlocations;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject LizardPrefab;
    [SerializeField] private float spawntime = 20f;
    private float currentwait = 0f;
    //[SerializeField] private float spawndecay = 2f;
    //[SerializeField] private float minspawntime = 3f;
    private AudioSource spawnsound;
    bool pieceacquired = false;
    //bool firstdead = false;


    // Start is called before the first frame update
    void Start()
    {
        spawnlocations = new List<Vector2>() { new Vector2(-48f, -22f), new Vector2(-48f, 22f), new Vector2(49f, 22f), new Vector2(48f, -22f) };
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
                for(int i = 0; i < 2; i++)
                {
                    List<Vector2> spawncopy = new List<Vector2>();
                    for(int j = 0; j < spawnlocations.Count; j++)
                    {
                        spawncopy.Add(spawnlocations[j]);
                    }
                    
                    int randnum = Random.Range(0, spawncopy.Count - 1);
                    transform.position = spawncopy[randnum];
                    Instantiate(EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                    spawncopy.RemoveAt(randnum);

                    randnum = Random.Range(0, spawncopy.Count - 1);
                    transform.position = spawncopy[randnum];
                    Instantiate(EnemyPrefab, this.gameObject.transform.position, Quaternion.identity);
                    spawncopy.RemoveAt(randnum);

                    randnum = Random.Range(0, spawncopy.Count - 1);
                    transform.position = spawncopy[randnum];
                    Instantiate(LizardPrefab, this.gameObject.transform.position, Quaternion.identity);
                }

                spawnsound.Play();

                currentwait = spawntime;
            }
        }
    }
}

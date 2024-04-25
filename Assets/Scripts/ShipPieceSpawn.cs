using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPieceSpawn : MonoBehaviour
{
    int deadenemies;
    [SerializeField] int killsneeded = 10;
    bool enoughkills;
    bool spawned;
    public GameObject shippiece;
    private Vector2 spawnpos;
    [SerializeField] public GameObject exitwall;

    public void AddKill(int numkilled)
    {
        deadenemies += numkilled;
    }

    public int GetKills()
    {
        return deadenemies;
    }

    public void SetPieceLocation(Vector2 enemypos)
    {
        spawnpos = enemypos;
    }
    
    public int GetNeededKills()
    {
        return killsneeded;
    }

    // Start is called before the first frame update
    void Start()
    {
        enoughkills = false;
        deadenemies = 0;
        spawned = false;
        spawnpos = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if(deadenemies >= killsneeded)
        {
            enoughkills = true;
        }

        if(enoughkills && !spawned)
        {
            GetComponent<EnemySpawn>().AcquirePiece();
            exitwall.SetActive(false);
            //shippiece.transform.position = spawnpos;
            //shippiece.SetActive(true);
            spawned = true;
        }
        
    }
}

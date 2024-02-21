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

    public void AddKill(int numkilled)
    {
        deadenemies += numkilled;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        enoughkills = false;
        deadenemies = 0;
        spawned = false;
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
            shippiece.SetActive(true);
            spawned = true;
        }
        
    }
}

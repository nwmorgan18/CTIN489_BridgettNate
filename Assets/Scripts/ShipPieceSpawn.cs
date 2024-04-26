using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShipPieceSpawn : MonoBehaviour
{
    int deadenemies;
    [SerializeField] int killsneeded = 10;
    bool enoughkills;
    bool spawned;
    public GameObject shippiece;
    private Vector2 spawnpos;
    [SerializeField] public GameObject exitwall;
    [SerializeField] public GameObject container;
    public TextMeshProUGUI textMeshPro;
    [SerializeField] Slider progressbar;

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
        progressbar.value = (float)(killsneeded-deadenemies) / (float)killsneeded;
        if(deadenemies >= killsneeded)
        {
            enoughkills = true;
        }

        if(enoughkills && !spawned)
        {
            Time.timeScale = 0;
            container.SetActive(true);
            UpdateText("Looks like there's a path up ahead!");
            
            GetComponent<EnemySpawn>().AcquirePiece();
            exitwall.SetActive(false);
            //shippiece.transform.position = spawnpos;
            //shippiece.SetActive(true);
            spawned = true;
        }
        
    }

    public void UpdateText(string newText)
    {
        // Assign the new text to the TextMeshPro component
        textMeshPro.text = newText;
    }

    public void next() {
        container.SetActive(false);
        Time.timeScale = 1;
    }
}

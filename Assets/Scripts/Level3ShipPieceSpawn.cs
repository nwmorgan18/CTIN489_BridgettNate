using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3ShipPieceSpawn : MonoBehaviour
{
    private bool bossdead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void KillBoss()
    {
        bossdead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossdead)
        {
            transform.position = new Vector2(0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //spawner.GetComponent<EnemySpawn>().AcquirePiece();
            //exitroute.SetActive(true);
            SceneManager.LoadScene("Ending");
            Destroy(this.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPieceControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject spawner;
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.GetComponent<EnemySpawn>().AcquirePiece();
            /*
            GameObject enemy;
            do
            {
                enemy = GameObject.FindWithTag("Enemy");
                Destroy(enemy);
                enemy = GameObject.FindWithTag("Enemy");
            } while (enemy != null);
            */
            Destroy(this.gameObject);
        }
    }
}

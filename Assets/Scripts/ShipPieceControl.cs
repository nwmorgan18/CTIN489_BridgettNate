using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPieceControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject exitwall;
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
            exitwall.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}

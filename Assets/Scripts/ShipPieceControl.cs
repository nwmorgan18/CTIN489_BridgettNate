using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPieceControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject exitwall;
    [SerializeField] public GameObject arrow;
    private GameObject spawner;
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
        arrow.SetActive(false);
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
            arrow.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}

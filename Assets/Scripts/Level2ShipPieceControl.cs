using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ShipPieceControl : MonoBehaviour
{
    [SerializeField] public GameObject exitroute;
    [SerializeField] private int lizardsleft = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lizardsleft <= 0)
        {
            exitroute.SetActive(true);
            //transform.position = new Vector2(0f, -6f);
        }
    }

    public void KillLizard()
    {
        lizardsleft--;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //spawner.GetComponent<EnemySpawn>().AcquirePiece();
            exitroute.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}

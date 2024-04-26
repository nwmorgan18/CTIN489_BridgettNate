using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2ShipPieceControl : MonoBehaviour
{
    [SerializeField] public GameObject exitroute;
    [SerializeField] private int lizardsleft = 8;
    [SerializeField] private Slider progressbar;

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
        progressbar.value = (float)lizardsleft / (float)8;
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

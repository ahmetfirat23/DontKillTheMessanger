using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : MonoBehaviour
{
    bool colliderBusy = false;
    int breadCount = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !colliderBusy)
        {
            colliderBusy = true;
            collision.GetComponent<PlayerManager>().Collision();
            Destroy(gameObject);
            breadCount--;
            if (breadCount <= 0)
            {
                collision.GetComponent<GameManager>().WinUtility();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliderBusy = false;
        }
    }
}

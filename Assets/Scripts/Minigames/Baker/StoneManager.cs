using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    bool colliderBusy = false;
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
            collision.GetComponent<GameManager>().GetDamage();
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

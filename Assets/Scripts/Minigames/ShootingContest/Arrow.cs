using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Arrow : MonoBehaviour
{
    CinemachineVirtualCamera vCam;
    CinemachineVirtualCamera vCam2;
    bool hasHit;
    public Rigidbody2D rb;

    ShootingGameManager gameManager;
    private void Start()
    {
        vCam2 = GameObject.Find("CMFull").GetComponent<CinemachineVirtualCamera>();
        vCam = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();

        gameManager = GameObject.Find("MiniGameManager").GetComponent<ShootingGameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(hasHit == false) {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        vCam.Priority = 10;
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        
        
        if (collision.collider.CompareTag("Apple"))
        {
            transform.parent = collision.transform;
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            collision.collider.tag = "Untagged";
            gameManager.IncreaseAppleShot();
            Destroy(collision.gameObject, 2f); //
        }
        else
        {
            gameManager.DecreaseLivesLeft();
        }
        Invoke(nameof(StartFollowingPlayer),1f);
    }
    void StartFollowingPlayer()
    {
        GameObject bow = GameObject.Find("Bow");
        vCam.Follow =bow.transform;
        bow.GetComponent<BowScript>().enabled = true;
        
    }
}

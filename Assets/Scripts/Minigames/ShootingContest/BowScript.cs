using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class BowScript : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 10f;
    Animator animator;
    public GameObject arrow;
    CinemachineVirtualCamera vCam;
    CinemachineVirtualCamera vCam2;

    

    Quaternion rot;

    public Transform shotPoint;

    Vector2 dir;
    [SerializeField] float maxWaitTime = 2f;

    public double pullStartTime = 0;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints = 30;
    public float spaceBetweenPoints = 0.025f;

    float launchForce;

    bool tryingThing = false;


    // Start is called before the first frame update
    void Start()
    {
        vCam2 = GameObject.Find("CMFull").GetComponent<CinemachineVirtualCamera>();
        vCam = GameObject.Find("CM").GetComponent<CinemachineVirtualCamera>();
        animator = GetComponent<Animator>();
        points = new GameObject[numberOfPoints];
    }

    void InstantiatePoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        if (this.enabled)
        {
            Vector2 value = context.ReadValue<Vector2>();
            dir = Camera.main.ScreenToWorldPoint(value) - transform.position;
            float angle = Mathf.Clamp(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, 0, 90);
            rot = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rot;
        }
    }
    
    float ClampTheForce()
    {
        return Mathf.Clamp((float)(Time.time - pullStartTime), 0, maxWaitTime);
    }
    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (this.enabled)
        {
            if (context.started)
            {
                animator.SetTrigger("ArrowDrawn");
                // knock
                pullStartTime = Time.time; // doubles
                InstantiatePoints();
                tryingThing = true;

            }
            launchForce = arrowSpeed * ClampTheForce();

            if (context.canceled && tryingThing)
            {

                GameObject newArrow = Instantiate(arrow, shotPoint.position, rot);
                Rigidbody2D rb2d = newArrow.GetComponent<Rigidbody2D>();

                vCam.Follow = newArrow.transform;
                vCam.Priority = 12;
                rb2d.AddRelativeForce(new Vector2(launchForce, 0), ForceMode2D.Impulse);
                pullStartTime = 0;
                DestroyPoints();
                tryingThing = false;
                this.enabled = false;
            }
        }
    }
    float getLaunchForce()
    {
        return arrowSpeed * ClampTheForce();
    }
    void DestroyPoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            Destroy(points[i]);
        }
    }
    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + dir.normalized * getLaunchForce() * t + 0.5f * Physics2D.gravity * t * t;
        return position;
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if(pullStartTime != 0) { 
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }
    }
}

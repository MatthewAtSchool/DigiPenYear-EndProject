using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObstacle : MonoBehaviour
{
    public GameObject cannonball;
    public Transform pointOfFire;

    public float cannonballSpeed;

    public float fireDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shooting", fireDelay, fireDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shooting()
    {
        GameObject obstacle = Instantiate(cannonball, pointOfFire.position, pointOfFire.rotation);
        obstacle.GetComponent<Rigidbody>().velocity = pointOfFire.forward * cannonballSpeed * Time.deltaTime;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestruction : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

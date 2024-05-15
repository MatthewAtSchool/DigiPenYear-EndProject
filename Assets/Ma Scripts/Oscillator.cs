using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;

    [Range(0, 1)] float movementFactor;

    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    [SerializeField] float rotationspeed;
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;
    [SerializeField] float zRotation;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period; // Continue to grow as the game plays
        float tau = Mathf.PI * 2f; //V alue of 6.283...
        float rawSinWave = Mathf.Sin(cycles * tau); // Gets value from -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f; // Recalculating to go from 0 to instead

        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;

        transform.Rotate(xRotation * Time.deltaTime, yRotation * Time.deltaTime, zRotation * Time.deltaTime);
    }
}
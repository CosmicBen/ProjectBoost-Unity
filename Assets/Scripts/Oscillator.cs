using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition = Vector3.zero;
    [SerializeField] private Vector3 movementVector = Vector3.zero;
    [SerializeField] private float period = 1.0f;


    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (Mathf.Abs(period) <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2.0f;
        float sinWave = Mathf.Sin(cycles * tau);

        Vector3 offset = movementVector * (0.5f * sinWave + 0.5f);
        transform.position = startingPosition + offset;
    }
}

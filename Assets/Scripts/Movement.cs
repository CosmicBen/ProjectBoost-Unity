using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float thrust = 1000.0f;
    [SerializeField] private float rotationThrust = 1.0f;
    [SerializeField] private AudioClip thrustSfx;
    [SerializeField] private ParticleSystem thrustVfx;
    [SerializeField] private ParticleSystem leftThrustVfx;
    [SerializeField] private ParticleSystem rightThrustVfx;

    private Rigidbody myRigidbody = null;
    private AudioSource myAudioSource = null;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    private void StartThrust()
    {
        myRigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);

        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(thrustSfx);
        }

        if (!thrustVfx.isPlaying)
        {
            thrustVfx.Play();
        }
    }

    private void StopThrust()
    {
        myAudioSource.Stop();
        thrustVfx.Stop();
    }

    private void ProcessRotation()
    {
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        if (left && !right)
        {
            RotateLeft();
        }
        else if (right && !left)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateLeft()
    {
        RotateRocket(Vector3.forward);
        rightThrustVfx.Stop();

        if (!leftThrustVfx.isPlaying)
        {
            leftThrustVfx.Play();
        }
    }

    private void RotateRight()
    {
        RotateRocket(Vector3.back);
        leftThrustVfx.Stop();

        if (!rightThrustVfx.isPlaying)
        {
            rightThrustVfx.Play();
        }
    }

    private void StopRotating()
    {
        leftThrustVfx.Stop();
        rightThrustVfx.Stop();
    }


    private void RotateRocket(Vector3 axis)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(axis * rotationThrust * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}

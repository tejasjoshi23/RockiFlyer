using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationThrust = 100;
    [SerializeField] AudioClip thrustEngine;
    [SerializeField] ParticleSystem mainThrustParticleSystem;
    [SerializeField] ParticleSystem frontThrusterParticleSystem;
    [SerializeField] ParticleSystem backThrusterParticleSystem;
    AudioSource rocketAudio;

    //[SerializeField] float Thrust = 1000;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Thrust applied ");
            rb.AddRelativeForce(Vector3.up*mainThrust*Time.deltaTime);  //shorthand for Vector3(0, 1, 0) (x, y, z)
            if(!rocketAudio.isPlaying)
            rocketAudio.PlayOneShot(thrustEngine);

            if(!mainThrustParticleSystem.isPlaying)
            mainThrustParticleSystem.Play();
        }
        else
        {
            rocketAudio.Stop();
            mainThrustParticleSystem.Stop();
            // mainThrustParticleSystem.Pause();
        }
    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Roatating forward");
            ApplyRotataion(rotationThrust);

            if(!backThrusterParticleSystem.isPlaying)
            backThrusterParticleSystem.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Roatating backward");
            ApplyRotataion(-rotationThrust);

            if(!frontThrusterParticleSystem.isPlaying)
            frontThrusterParticleSystem.Play();
        }
        else
        {
            frontThrusterParticleSystem.Stop();
            backThrusterParticleSystem.Stop();
        }
    }

    void ApplyRotataion(float rotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        rb.freezeRotation = false;
    }

}

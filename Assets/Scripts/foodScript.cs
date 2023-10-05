using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource collisionAudio;

    private void Start()
    {
        collisionAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionAudio.Play();
    }
}

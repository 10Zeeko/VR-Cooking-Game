using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum foodType
{
    Bread,
    HotDog,
    Ketchup,
    Musterd
}

public class foodScript : MonoBehaviour
{
    public foodType selectedFood;

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

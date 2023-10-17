using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum to represent different types of food
public enum FoodType
{
    Bread,
    HotDog,
    Ketchup,
    Mustard
}

public class FoodScript : MonoBehaviour
{
    public FoodType SelectedFood;

    [SerializeField]
    private AudioSource _collisionAudio;

    private void Start()
    {
        _collisionAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionAudio.Play();
    }
}

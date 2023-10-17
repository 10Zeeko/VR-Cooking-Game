using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class ReceiptCheck : MonoBehaviour
{
    [SerializeField]
    private foodScript[] receta;
    [SerializeField]
    private foodType[] FinalResult;

    [SerializeField]
    private Texture2D[] ingredientImages;
    [SerializeField]
    private GameObject[] canvasImages;

    [SerializeField]
    private GameObject win;
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private ParticleSystem loseSmoke;

    [SerializeField]
    private AudioSource winPlayAudio;
    [SerializeField] 
    private AudioSource losePlayAudio;
    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip loseClip;
    [SerializeField]
    private Cook_Timer _timer;

    void Start()
    {
        FinalResult = new foodType[4];
        var values = Enum.GetValues(typeof(foodType)).Cast<foodType>().ToList();
        for (int i = 0; i < FinalResult.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, values.Count);
            FinalResult[i] = values[randomIndex];
            values.RemoveAt(randomIndex);
        }
    
        for (int i = 0; i < FinalResult.Length; i++)
        {
            var imageComponent = canvasImages[i].GetComponent<UnityEngine.UI.Image>();
            imageComponent.sprite = Sprite.Create(ingredientImages[(int)FinalResult[i]], new Rect(0, 0, ingredientImages[(int)FinalResult[i]].width, ingredientImages[(int)FinalResult[i]].height), Vector2.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if 'other' and 'other.gameObject' are not null
        if (other != null && other.gameObject != null)
        {
            // Add objets with 'food' tag
            if (other.gameObject.CompareTag("food"))
            {
                var foodScriptComponent = other.gameObject.GetComponent<foodScript>();
                if (foodScriptComponent != null)
                {
                    for (int i = 0; i < receta.Length; i++)
                    {
                        if (receta[i] == null)
                        {
                            receta[i] = foodScriptComponent;
                            break;
                        }
                    }
                }
            }

            bool isSame = true;
            for (int i = 0; i < receta.Length; i++)
            {
                // Check if 'receta[i]' is not null
                if (receta[i] == null || receta[i].selectedFood != FinalResult[i])
                {
                    isSame = false;
                    break;
                }
            }

            if (isSame)
            {
                for (int i = 0; i < receta.Length; i++)
                {
                    // Check if 'receta[i]' and 'receta[i].gameObject' are not null
                    if (receta[i] != null && receta[i].gameObject != null)
                    {
                        receta[i].gameObject.SetActive(false);
                        receta[i] = null;
                    }
                }

                smoke.Play();
                Instantiate(win, transform.position, Quaternion.identity);
                winPlayAudio.Play();
                Debug.Log("RECETA COMPLETA");

                // Check if '_timer' is not null
                if (_timer != null)
                {
                    _timer.completed = true;
                }
            }
            else
            {
                bool isNotNull = true;
                for (int i = 0; i < receta.Length; i++)
                {
                    if (receta[i] == null)
                    {
                        isNotNull = false;
                    }
                }

                if (isNotNull)
                {
                    loseSmoke.Play();
                    losePlayAudio.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // En caso de que salgan se borran de la lista
        if (other.gameObject.CompareTag("food"))
        {
            for (int i = 0; i < receta.Length; i++)
            {
                if (receta[i].gameObject == other.gameObject)
                {
                    receta[i] = null;
                    // Mover la lista para que no quede un hueco en medio
                    for (int j = i; j < receta.Length - 1; j++)
                    {
                        receta[j] = receta[j + 1];
                    }
                    receta[receta.Length - 1] = null;
                    break;
                }
            }
        }
    }
}


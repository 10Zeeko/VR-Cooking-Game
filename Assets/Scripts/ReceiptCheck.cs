using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ReceiptCheck : MonoBehaviour
{
    [Header("Randomize Receipt Variables")]
    [SerializeField]
    private FoodScript[] _recipe;
    [SerializeField]
    private FoodType[] _finalResult;

    [SerializeField]
    private Texture2D[] _ingredientImages;
    [SerializeField]
    private GameObject[] _canvasImages;

    [Header("Win/Lose")]
    [SerializeField]
    private GameObject _win;
    [SerializeField]
    private ParticleSystem _smoke;
    [SerializeField]
    private ParticleSystem _loseSmoke;

    [SerializeField]
    private AudioSource _winPlayAudio;
    [SerializeField]
    private AudioSource _losePlayAudio;

    [Header("Timer")]
    [SerializeField]
    private CookTimer _timer;


    void Start()
    {
        // Initialize the final result with random food types
        _finalResult = new FoodType[4];
        var values = Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToList();
        for (int i = 0; i < _finalResult.Length; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, values.Count);
            _finalResult[i] = values[randomIndex];
            values.RemoveAt(randomIndex);
        }

        // Assign the corresponding images to the canvas
        for (int i = 0; i < _finalResult.Length; i++)
        {
            var imageComponent = _canvasImages[i].GetComponent<UnityEngine.UI.Image>();
            imageComponent.sprite = Sprite.Create(_ingredientImages[(int)_finalResult[i]], new Rect(0, 0, _ingredientImages[(int)_finalResult[i]].width, _ingredientImages[(int)_finalResult[i]].height), Vector2.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the 'food' tag
        if (other.gameObject.CompareTag("food"))
        {
            var foodScriptComponent = other.gameObject.GetComponent<FoodScript>();
            if (foodScriptComponent != null)
            {
                // Add the food to the first empty slot in the recipe
                for (int i = 0; i < _recipe.Length; i++)
                {
                    if (_recipe[i] == null)
                    {
                        _recipe[i] = foodScriptComponent;
                        break;
                    }
                }
            }

            // Check if the current recipe matches the final result
            bool isSame = true;
            for (int i = 0; i < _recipe.Length; i++)
            {
                if (_recipe[i] == null || _recipe[i].SelectedFood != _finalResult[i])
                {
                    isSame = false;
                    break;
                }
            }

            // If the recipe is correct, play the win animation and sound, and set the timer to completed
            if (isSame)
            {
                for (int i = 0; i < _recipe.Length; i++)
                {
                    if (_recipe[i] != null && _recipe[i].gameObject != null)
                    {
                        _recipe[i].gameObject.SetActive(false);
                        _recipe[i] = null;
                    }
                }

                _smoke.Play();
                Instantiate(_win, transform.position, Quaternion.identity);
                _winPlayAudio.Play();

                if (_timer != null)
                {
                    _timer.Completed = true;
                }
            }
            else
            {
                // If all recipe slots are filled but the recipe is incorrect, play the lose animation and sound
                bool isNotNull = true;
                for (int i = 0; i < _recipe.Length; i++)
                {
                    if (_recipe[i] == null)
                    {
                        isNotNull = false;
                    }
                }

                if (isNotNull)
                {
                    _loseSmoke.Play();
                    _losePlayAudio.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object has the 'food' tag
        if (other.gameObject.CompareTag("food"))
        {
            // Remove the food from the recipe and shift the remaining foods to fill the gap
            for (int i = 0; i < _recipe.Length; i++)
            {
                if (_recipe[i].gameObject == other.gameObject)
                {
                    _recipe[i] = null;

                    for (int j = i; j < _recipe.Length - 1; j++)
                    {
                        _recipe[j] = _recipe[j + 1];
                    }

                    _recipe[_recipe.Length - 1] = null;
                    break;
                }
            }
        }
    }
}

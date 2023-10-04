using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ReceiptCheck : MonoBehaviour
{
    [SerializeField]
    GameObject[] receta;
    [SerializeField]
    GameObject[] FinalResult;

    [SerializeField]
    GameObject win;
    [SerializeField]
    ParticleSystem smoke;

    private void OnTriggerEnter(Collider other)
    {
        // Añade los objetos que tengan la tag comida dentro de receta
        if (other.gameObject.CompareTag("food"))
        {
            for (int i = 0; i < receta.Length; i++)
            {
                if (receta[i] == null)
                {
                    receta[i] = other.gameObject;
                    break;
                }
            }
        }
        bool isSame = true;
        for (int i = 0; i < receta.Length; i++)
        {
            if (receta[i] != FinalResult[i])
            {
                isSame = false;
                break;
            }
        }
        if (isSame)
        {
            for (int i = 0; i < receta.Length; i++)
            {
                receta[i].gameObject.SetActive(false);
                receta[i] = null;
            }
            smoke.Play();
            Instantiate(win, transform.position, Quaternion.identity);
            Debug.Log("RECETA COMPLETA");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // En caso de que salgan borrarlos de la lista
        if (other.gameObject.CompareTag("food"))
        {
            for (int i = 0; i < receta.Length; i++)
            {
                if (receta[i] == other.gameObject)
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


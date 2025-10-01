using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MunicionEscopeta : MonoBehaviour
{
    
    
    public float velocidadDeDestruccion;

    void Start()
    {
        

        Destroy(gameObject,velocidadDeDestruccion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            Destroy(gameObject);
        }
    }
}

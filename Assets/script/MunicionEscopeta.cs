using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionEscopeta : MonoBehaviour
{
    
    private int valorPorCaja;

    void Start()
    {
        valorPorCaja = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            Destroy(gameObject);
        }
    }
}

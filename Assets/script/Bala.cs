using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D balaRB;
    private float tiempoDeDestruccion;
    public float danio;

    private void Awake()
    {
        balaRB = GetComponent<Rigidbody2D>();
        tiempoDeDestruccion = 5f;
    }

    //a este metodo lo va a llamar desde el scritp del cañon por eso lo puse en public

    public void LanzarBala(Vector2 direccion)
    {
        balaRB.velocity = direccion * speed;
        Destroy(gameObject,tiempoDeDestruccion);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Enemigo zombie = collision.gameObject.GetComponent<Enemigo>();

            zombie.RecibirDanio(danio);
            Destroy(gameObject);
        }
    }

}

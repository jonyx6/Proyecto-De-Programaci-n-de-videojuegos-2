using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Arma : MonoBehaviour
{
    private Camera m_Camera;
    [SerializeField, Range(1f, 20f)] float speedRotation;
    [SerializeField] private Bala bala;
    [SerializeField] private Transform posicionDeTiro;
    private SpriteRenderer srArma;
    public int cantDeBalas;
    [SerializeField] private Personaje personaje;

    void Start()
    {
        m_Camera = Camera.main;
        srArma = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        Vector2 cordenadasDelMouse = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = cordenadasDelMouse - (Vector2)transform.position;
        transform.right = Vector2.MoveTowards(transform.right, direccion, speedRotation * Time.deltaTime);

        DispararSiHayBalas(posicionDeTiro, bala);
        DirecionDeAnimacion(direccion);
        Recargar();

    }

    void DispararSiHayBalas(Transform unaPosicionDeTiro, Bala unaBala)
    {
        if (cantDeBalas >0)
        {
            Disparar(unaPosicionDeTiro,unaBala);
        }

    }

    void Disparar(Transform unaPosicionDeTiro, Bala unaBala)
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Bala balaInstanciada = Instantiate(unaBala, unaPosicionDeTiro.position, transform.rotation);
            balaInstanciada.LanzarBala(transform.right);
            cantDeBalas--;
        }
    }

    void Recargar()
    {
        if (cantDeBalas==0 && Input.GetMouseButtonDown(1) && personaje.cantDeMunicion >0)
        {
            cantDeBalas = 12;
            personaje.cantDeMunicion--; 
        }
        
    }

    void DirecionDeAnimacion(Vector2 unaDireccion)
    {
        if (unaDireccion.y < 0.0f)
        {
            srArma.flipY = true;
        }
        else if (unaDireccion.x > 0.0f)
        {
            srArma.flipY= false;
        }

    }
}

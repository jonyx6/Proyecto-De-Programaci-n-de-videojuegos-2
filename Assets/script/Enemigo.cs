using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public float speed;
    public float VidaMaxima;
    private float Vida;
    public Spawner sd;
    private Rigidbody2D rb;
    private GameObject objtivo;
    private SpriteRenderer SpriteRendererEnemy;
    public float danio;
    public Image BarraDeVida;
    public MunicionEscopeta MunicionEscopeta;
    private Vector2 direccion;
    private Coroutine dañoContinuo;

    // aplicando daño cada cierto tiempo.
    private int cantidadDeDaño = 10;
    private float tiempoPararVolverAGenerarDaño = 3f;
    private bool estaColisionando = false ;
    private float tiempoQuePasoDesdeElUltimoDaño = 0f;
    void Start()
    {
        Vida = VidaMaxima;
        objtivo = GameObject.FindGameObjectWithTag("Personaje");
        rb = GetComponent<Rigidbody2D>();
        SpriteRendererEnemy = GetComponent<SpriteRenderer>(); // ✅ Asignación correcta
    }

    public void RecibirDanio(float danio)
    {
        Vida -= danio;
        BarraDeVida.fillAmount = Vida / VidaMaxima;

        if (Vida < 1)
        {
            Muerte();
        }
    }

    private void FixedUpdate()
    {
        if (objtivo != null)
        {
            // rb.velocity = (objtivo.transform.position - transform.position).normalized * speed;
            Vector2 direccionEnemigo = objtivo.transform.position - transform.position;
            rb.velocity = direccionEnemigo * speed * Time.deltaTime;
        }
    }

    void Muerte()
    {
        float probabilidadDeSpawneo = 0.3f;
        GameManager.Instance.EnemigoDerrotado();

        if (UnityEngine.Random.value < probabilidadDeSpawneo)
        {
            Instantiate(MunicionEscopeta, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        sd.RestarEnemigo();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            estaColisionando = true;
            tiempoQuePasoDesdeElUltimoDaño = tiempoPararVolverAGenerarDaño;
        }
    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            estaColisionando = false;
            tiempoQuePasoDesdeElUltimoDaño = 0f;

        }
    }




    void Update()
    {
        if(objtivo != null)
        {
            direccion = (objtivo.transform.position - transform.position).normalized;
            volverAtacarCuandoPaseElTiempo();
            if (direccion.x < 0)
            {
                SpriteRendererEnemy.flipX = true;
            }
            if (direccion.x > 0)
            {
                SpriteRendererEnemy.flipX = false;
            }

        }

    }

    void volverAtacarCuandoPaseElTiempo()
    {
        if (estaColisionando)
        {
            tiempoQuePasoDesdeElUltimoDaño += Time.deltaTime;

            if (tiempoQuePasoDesdeElUltimoDaño >= tiempoPararVolverAGenerarDaño)
            {
                Personaje objetivo = objtivo.gameObject.GetComponent<Personaje>();
                objetivo.RecibirDanio(danio);
                tiempoQuePasoDesdeElUltimoDaño = 0f;
            }
        }
    }
}

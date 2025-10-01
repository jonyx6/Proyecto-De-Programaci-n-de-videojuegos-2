using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using Random = UnityEngine.Random;
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
    public float probabilidadDeSpawneoDeItem;
    private AudioSource aSource;
    public AudioClip[] sonidos;

    // aplicando daño cada cierto tiempo.
    public float tiempoPararVolverAGenerarDaño ;
    private bool estaColisionando = false ;
    private float tiempoQuePasoDesdeElUltimoDaño = 0f;
    void Start()
    {
        Vida = VidaMaxima;
        objtivo = GameObject.FindGameObjectWithTag("Personaje");
        rb = GetComponent<Rigidbody2D>();
        SpriteRendererEnemy = GetComponent<SpriteRenderer>(); 
        aSource = GetComponent<AudioSource>();  
       
        ReproducirSonidoAleatorio();
    }


    public void ReproducirSonidoAleatorio()
    {
        // se le puede agregar mas sonidos al zombi y reproducirlo de manera aleatoria

        if (sonidos.Length == 0) return;

        int index = Random.Range(0, sonidos.Length);
        aSource.clip = sonidos[index];
        aSource.Play();
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
        //float probabilidadDeSpawneo = 0.3f;
        GameManager.Instance.EnemigoDerrotado();

        if (UnityEngine.Random.value < probabilidadDeSpawneoDeItem)
        {
            Instantiate(MunicionEscopeta, transform.position, Quaternion.identity);
        }
        aSource.mute = true;   
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rbPersonaje;
    private SpriteRenderer srPersonaje;
    public float VidaMaxima;
    public float Vida;
    public Image BarraDeVida;
    private Animator aPersonaje;
    private bool estaVivo =  true;
    public  int cantDeMunicion;

    private void Start()
    {
        rbPersonaje = GetComponent<Rigidbody2D>();
        srPersonaje = GetComponent<SpriteRenderer>();
        Vida = VidaMaxima;
        aPersonaje = GetComponent<Animator>();
    }




    // Update is called once per frame
    void Update()
    {
        
            float inputHorizontal = Input.GetAxisRaw("Horizontal");
            float inputVertical = Input.GetAxisRaw("Vertical");
            Vector2 direccion = new Vector2(inputHorizontal, inputVertical).normalized;
            rbPersonaje.velocity = direccion * speed;
            DirecionDeAnimacion(direccion);
            CambiarAnimacion(direccion);
     
       
        
    }
    //funcion que evita que el personaje salga de la pantalla

    void LateUpdate()
    {
        Camera cam = Camera.main;
        float altura = cam.orthographicSize * 2f;
        float ancho = altura * cam.aspect;

        float minX = cam.transform.position.x - ancho / 2f;
        float maxX = cam.transform.position.x + ancho / 2f;
        float minY = cam.transform.position.y - altura / 2f;
        float maxY = cam.transform.position.y + altura / 2f;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }


    void CambiarAnimacion(Vector2 unaDireccion)
    {
        if(unaDireccion.x !=0f || unaDireccion.y != 0f)
        {
            aPersonaje.SetBool("puedeCaminar", true);
        }
        else
        {
            aPersonaje.SetBool("puedeCaminar",false);
        }
    }

    void DirecionDeAnimacion(Vector2 unaDireccion)
    {
        if(unaDireccion.x < 0.0f) {
            srPersonaje.flipX = true;
        }else if(unaDireccion.x > 0.0f)
        {
            srPersonaje.flipX = false;
        }
          
    }


    public void RecibirDanio(float danio)
    {
        Vida -= danio;
        Vida = Mathf.Clamp(Vida, 0, VidaMaxima);
       
        BarraDeVida.fillAmount = Vida / VidaMaxima;

        if (Vida < 1 )
        {
            estaVivo = false;
            Muerte();

        }
    }

    void Muerte()
    {
        if (!estaVivo)
        {
            Debug.Log("muere en la clase personaje y no en el gamemanager");
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                Debug.Log("gameManager no esta asignado");
            }
            
            Destroy(gameObject,0.5f);
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MunEscopeta"))
        {
            MunicionEscopeta municion = collision.gameObject.GetComponent<MunicionEscopeta>();
            cantDeMunicion += 1;
            
        }
    }



}

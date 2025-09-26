using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public Personaje instanciaPersonaje;
    
    public bool juegoActivo = true;
    private int oleadaActual = 1;
    private int enemigosDerrotados = 0;
    [SerializeField] private Timer tiempo;
    [SerializeField] private Spawner spawner;
    public float tiempoParaAumentarEnemigos;
    public GameObject menuPausa;
    private bool juegoPausado = false;

    // HUD
    [SerializeField] private Arma unArma;
    [SerializeField] private TMP_Text textoDeLaBala;
    [SerializeField] private TMP_Text textoDeMunicion;
    [SerializeField] private Personaje Personaje;
    [SerializeField] private TMP_Text textoEnemigosDerrotados;
    [SerializeField] private TMP_Text textoOleadas;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if (Instance != this) 
        {
            Destroy(gameObject);
        }

        // no se destruye en las cargas de la escena , cuando cambiamos de escena o iniciamos la misma no destruimos el game manager
        DontDestroyOnLoad(gameObject);
    }
    // metodo de prueba , borrar al finalizar su uso!!!!
    public void GameOver()
    {
       
        if (instanciaPersonaje.Vida < 1)
        {
            Debug.Log("muere en el gamemanager");
        }
    }

    public void EnemigoDerrotado()
    {
        enemigosDerrotados++;
    }

    public void AvanzarOleada()
    {
        oleadaActual++;
        if (oleadaActual > 4)
        {
            juegoActivo = false;
            Debug.Log("¡Ganaste! Completaste todas las oleadas.");
        }
        else
        {
            Debug.Log("Oleada " + oleadaActual + " iniciada.");
            tiempoParaAumentarEnemigos = 60.0F;

        }
    }



    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    private void Update()
    {
        textoDeLaBala.text = unArma.cantDeBalas.ToString();
        textoDeMunicion.text = Personaje.cantDeMunicion.ToString();
        textoEnemigosDerrotados.text = enemigosDerrotados.ToString();
        textoOleadas.text = oleadaActual.ToString();
        PausarSiElBotonFuePrecionado();
        AumentarOleadas();
    }

    public void PausarSiElBotonFuePrecionado()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }


    void AumentarOleadas()
    {
        if (tiempo.Segundos >= tiempoParaAumentarEnemigos)
        {
            spawner.maxEnemigos +=1;
            tiempoParaAumentarEnemigos *=2 ;
        }
    }

}

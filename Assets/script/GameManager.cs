
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject gameOverPanel;
    public GameObject victoriaPanel;
    public Enemigo audioEnemigo;

    // barra De Resignacion 
    private float cantDeResignacion = 0;
    [SerializeField] private Image barraDeResiganacion;
    public float velocidadDeResignacion = 0.2f;

    // HUD
    [SerializeField] private Arma unArma;
    [SerializeField] private TMP_Text textoDeLaBala;
    [SerializeField] private TMP_Text textoDeMunicion;
    [SerializeField] private Personaje Personaje;
    [SerializeField] private TMP_Text textoEnemigosDerrotados;
    [SerializeField] private TMP_Text textoOleadas;

    //gameover text

    [SerializeField] private TMP_Text gamaOverEnemigosDerrotados;
    [SerializeField] private TMP_Text gameOverOleadas;

    // victoria text

    [SerializeField] private TMP_Text victoriaEnemigosDerrotados;
    [SerializeField] private TMP_Text victoriaOleadas;



    // audio
    //private AudioSource aSource;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if (Instance != this) 
        {
            Destroy(gameObject);
        }

        AudioListener.pause = false;


    }
    
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
            victoriaPanel.SetActive(true);
            AudioListener.pause = true;
        }
        else
        {
            Debug.Log("Oleada " + oleadaActual + " iniciada.");
            spawner.maxEnemigos += 2;
            tiempoParaAumentarEnemigos = 60.0F;

        }
    }

    void AumentarResignacion()
    {
        if (MunicionEscasa())
        {
            IncrementarBarra();

            DetenerPersonajeSiLaBarraSeLLeno();
            Debug.Log("barra aumentando");

        }

        if (Personaje.cantDeMunicion > 3)
        {
            Debug.Log("la barra se disminuye");
            DisminuirBarra();
        }

        if (Personaje.cantDeMunicion == 3)
        {
            Debug.Log("la barra Se Detuvo");
            DetenerBarra();

        }
        
        if (instanciaPersonaje.cantDeMunicion < 1 && unArma.cantDeBalas == 0)
        {
            Debug.Log("velocidad DE resignacin aumentada");
            this.velocidadDeResignacion += 0.0001f;
        }
    }
    

    void IncrementarBarra()
    {
        cantDeResignacion += Time.deltaTime * velocidadDeResignacion; // en este momento son 2 segundos pero se puede cambiar en el inspector

        cantDeResignacion = Mathf.Clamp01(cantDeResignacion);// asegura que no pase de 1.

        barraDeResiganacion.fillAmount = cantDeResignacion;
    }

    void DetenerPersonajeSiLaBarraSeLLeno()
    {
        if (cantDeResignacion >= 1f)
        {
            Personaje.puedeMoverse = false;
            Debug.Log("El personaje se ha resignado.");

        }
    }

    void DetenerBarra()
    {
        barraDeResiganacion.fillAmount = cantDeResignacion;
    }
    void DisminuirBarra()
    {
        barraDeResiganacion.fillAmount = cantDeResignacion;
        cantDeResignacion -= Time.deltaTime * velocidadDeResignacion;
    }


    // sin uso actualmente 
    bool SinBalas()
    {
        return unArma.cantDeBalas  == 0;
    }

    bool MunicionEscasa()
    {
        return Personaje.cantDeMunicion <= 2;
    }

   
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }


    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
        AudioListener.pause = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
        AudioListener.pause = true;
    }

    public void Salir()
    {
        Debug.Log("saliendo del juego");
        Application.Quit();
    }

    // update
    private void Update()
    {
        textoDeLaBala.text = unArma.cantDeBalas.ToString();
        textoDeMunicion.text = Personaje.cantDeMunicion.ToString();
        textoEnemigosDerrotados.text = enemigosDerrotados.ToString();
        textoOleadas.text = oleadaActual.ToString();
        gameOverOleadas.text = oleadaActual.ToString();
        gamaOverEnemigosDerrotados.text = enemigosDerrotados.ToString();
        victoriaEnemigosDerrotados.text = enemigosDerrotados.ToString();
        victoriaOleadas.text = oleadaActual.ToString();
        PausarSiElBotonFuePrecionado();
        ActivarGameOverSiElPersonajeMurio();
        AumentarOleadas();
        AumentarResignacion();
    }

    public void ActivarGameOverSiElPersonajeMurio()
    {
        if (Personaje.Vida < 1)
        {
            MostrarGameOver();

            
        }
    }

    void MostrarGameOver()
    {
        gameOverPanel.SetActive(true);
        AudioListener.pause = true;
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

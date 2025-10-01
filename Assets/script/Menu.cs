using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    public AudioSource aMenu;

    void Awake()
    {
        aMenu = GetComponent<AudioSource>();
        if (aMenu != null)
            aMenu.Play();
        else
            Debug.LogWarning("AudioSource no encontrado en el objeto Menu.");
    }
    public void Jugar()
    {
        aMenu.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("saliendo del juego");
        Application.Quit();
    }



}

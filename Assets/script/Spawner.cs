using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemigo enemigo;
    public int maxEnemigos ;
    public int enemigosEnPantalla ;

    void Update()
    {
        if (enemigosEnPantalla < maxEnemigos)
        {
            float randomY = Random.Range(0f, Screen.height);
            float ladoX = Random.Range(0, 2) == 0 ? 0f : Screen.width;// if compacto ...si el numeor es 0 entonces ladoX =0 de lo contrario es el screen.width
            Vector3 screenPos = new Vector3(ladoX, randomY, Camera.main.farClipPlane / 2f); // Z más profundo
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            Enemigo enemigoInstanciado = Instantiate(enemigo, worldPos, Quaternion.identity);
            enemigoInstanciado.sd = this;
            enemigosEnPantalla++;


        }


    }

    public void RestarEnemigo()
    {
        enemigosEnPantalla -= 1;

    }
}

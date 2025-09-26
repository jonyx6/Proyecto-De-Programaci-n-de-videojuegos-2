
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private TMP_Text textoDelTimer;
    private float cuentaDeTiempo;
    private int minutos, segundos, centecimas;
    public float Segundos => cuentaDeTiempo;

    public void Update()
    {
        cuentaDeTiempo += Time.deltaTime;
        minutos = (int)(cuentaDeTiempo / 60f);
        segundos = (int)(cuentaDeTiempo - minutos * 60f);
        centecimas = (int)((cuentaDeTiempo - (int)cuentaDeTiempo) * 100f);
        textoDelTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centecimas);

        if(cuentaDeTiempo >= 300f)// 5 minutos 
        {
            GameManager.Instance.AvanzarOleada();
            cuentaDeTiempo = 0;
        }
        

    }

    public float ObtenerSegundos()
    {
        return cuentaDeTiempo;
    }

}



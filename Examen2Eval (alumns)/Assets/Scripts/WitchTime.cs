using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTime : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioClip witchTimeClip;

    private bool isWitchTimeActive = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !isWitchTimeActive)//Verifica si el jugador ha pulsado el boton derecho del ratón.
        {
            StartCoroutine(StartWitchTime());//Iniciar el efecto de Witch Time.
        }
    }

    IEnumerator StartWitchTime()
    {
        isWitchTimeActive = true;
        audioManager.PlayAudio(witchTimeClip, "witchTime");//Reproducir el sonido de Witch Time.
        Time.timeScale = 0.25f;//Reduce la escala de tiempo del juego.

        yield return new WaitForSeconds(witchTimeClip.length);//Espera que el sonido termine de reproducirse.
        Time.timeScale = 1f;//Restaurar la escala de tiempo del juego a su valor normal.
        isWitchTimeActive = false;
    }
}

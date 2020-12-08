using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDeUI : MonoBehaviour
{
    public GameObject gLevelName;
    public GameObject gEliminado;
    public GameObject gTime;
    public GameObject gTimer;

    public GameObject gEliminadoJ1;
    public GameObject gEliminadoJ2;

    public Text tEliminados;
    public string sFormat = "{0}/{1}";

    public Text tTime;
    public string sTimeFormat = "{0}:{1}";

    public int total = 2;
    public int delete = 0;

    public Text tTimer;
    private float totalTime = 105f;

    private void Awake()
    {
        tEliminados.text = string.Format(sFormat, delete, total);
        tTime.text = string.Format(sTimeFormat, 1, 45);
    }

    private void Update()
    {
        if (ControladorJuego.iniciarjuego)
        {
            totalTime -= Time.deltaTime;
            Debug.Log("Time->" + totalTime);

            if (totalTime <= 0.0f)
            {
                ControladorJuego.iniciandoJuego = false;
                ControladorJuego.iniciarjuego = false;
                ControladorJuego.reiniciarJuego();
            }
            else
            {
                setTime();
            }
        }

    }

    public void setDelete() {
        delete++;
        tEliminados.text = string.Format(sFormat, delete, total);
        if(delete >= total)
        {
            ControladorJuego.iniciandoJuego = false;
            ControladorJuego.iniciarjuego = false;
            ControladorJuego.reiniciarJuego();
        }
    }

    public void setTime()
    {
        int minutes = (int)(totalTime / 60f);
        int seconds = Mathf.RoundToInt(((totalTime / 60f) - minutes)*60);
        tTime.text = string.Format(sTimeFormat, minutes, seconds);
    }

    public void setCount(string count)
    {
        tTimer.text = count;
    }

    public void showMenu()
    {
        gLevelName.SetActive(true);
        gEliminado.SetActive(true);
        gTime.SetActive(true);
        gTimer.SetActive(true);
    }

    public void hideCount()
    {
        gTimer.SetActive(false);
    }

    public void showEliminadoJ1()
    {
        gEliminadoJ1.SetActive(true);
    }

    public void showEliminadoJ2()
    {
        gEliminadoJ2.SetActive(true);
    }
}

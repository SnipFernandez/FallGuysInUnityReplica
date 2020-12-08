using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorJuego : MonoBehaviour
{
    public Transform posicionCamara;
    public bool cambiarCamara = false;
    public bool cambiadoCamara = false;
    public int indiceposicion = 0;
    public float tiempodeCambio = 2;
    public float tiempoTranscurrido = 0;
    public PathCreator path;
    public Transform pj1;
    public Transform pj2;
    public Camera camera1;
    public Camera camera2;
    public ControladorDeObstaculos cdObs;
    public Animator UIanimator;
    public ControladorDeUI controladorDeUI;
    public static bool iniciarjuego = false;
    public static bool iniciandoJuego = false;

    private CambiosCamara[] cambios = new CambiosCamara[] {
        new CambiosCamara()
        {
            posicion = new Vector3(44.1f,29,39.6f),
            rotacion = Quaternion.Euler(30.7f,-127.3f,0),
            tipo = CambiosCamara.TiposCambioCamara.CAMBIOPOSICIONYROTACION,
            conAnimacion = false
        },
        new CambiosCamara()
        {
            posicion = new Vector3(48f,23.9f,-36.6f),
            rotacion = Quaternion.Euler(26.5f,-51.2f,0),
            tipo = CambiosCamara.TiposCambioCamara.CAMBIOPOSICIONYROTACION,
            conAnimacion = false
        },
        new CambiosCamara()
        {
            posicion = new Vector3(61.9f,19.5f,-29.5f),
            rotacion = Quaternion.Euler(0,-90,0),
            tipo = CambiosCamara.TiposCambioCamara.CAMBIOPOSICIONYROTACION,
            conAnimacion = false
        },
        new CambiosCamara()
        {
            posicion = new Vector3(61.9f,19.5f,35.71f),
            rotacion = Quaternion.Euler(0,-90,0),
            tipo = CambiosCamara.TiposCambioCamara.CAMBIOPOSICIONYROTACION,
            conAnimacion = true
        },
        new CambiosCamara()
        {
            posicion = new Vector3(-80f,10,0),
            rotacion = Quaternion.Euler(0,90,0),
            tipo = CambiosCamara.TiposCambioCamara.SEGUIMIENTORUTA,
            endOfPathInstruction= EndOfPathInstruction.Stop,
            conAnimacion = false
        },
        new CambiosCamara()
        {
            posicion = new Vector3(12,12,0),
            rotacion = Quaternion.Euler(40,-90,0),
            tipo = CambiosCamara.TiposCambioCamara.ASIGNARPADRE,
            conAnimacion = false
        }

    };
    // Start is called before the first frame update
    void Start()
    {
        if(cambios != null && cambios.Length > 0)
        {
            posicionCamara.position = cambios[0].posicion;
            posicionCamara.rotation = cambios[0].rotacion;
            cambios[cambios.Length - 2].path = path;
            cambios[cambios.Length - 1].padre = pj1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!iniciandoJuego)
        {
            if (!cambiarCamara && !cambiadoCamara)
            {
                tiempoTranscurrido += Time.deltaTime;
                if (tiempoTranscurrido >= tiempodeCambio)
                {
                    cambiarCamara = true;
                    tiempoTranscurrido = 0;
                }
            }

            if (cambiarCamara && !cambiadoCamara)
            {
                cambiarCamara = false;
                indiceposicion++;
                if (cambios != null && cambios.Length > indiceposicion)
                {
                    if (indiceposicion >= 4)
                    {
                        UIanimator.SetInteger("indexAnimacion", 1);
                    }
                    StartCoroutine(cambiarPosicionCamara(cambios[indiceposicion]));
                }
            }

            if (cambios.Length <= indiceposicion)
            {
                // iniciamos el juego.
                camera1.rect = new Rect(0, 0, .5f, 1);
                camera2.rect = new Rect(.5f, 0, .5f, 1);

                camera1.gameObject.transform.parent = pj1;
                camera1.gameObject.transform.localPosition = posicionCamara.localPosition;
                camera1.gameObject.transform.localRotation = posicionCamara.localRotation;

                camera2.gameObject.transform.parent = pj2;
                camera2.gameObject.transform.localPosition = posicionCamara.localPosition;
                camera2.gameObject.transform.localRotation = posicionCamara.localRotation;

                controladorDeUI.showMenu();

                iniciandoJuego = true;
                StartCoroutine(CuentaAtras());

                //cdObs.PuedeGenerarObstaculos = true;
                //iniciarjuego = true;
                //gameObject.SetActive(false);
            } 
        }
    }

    public IEnumerator CuentaAtras() {
        controladorDeUI.setCount("3");
        yield return new WaitForSeconds(1);
        controladorDeUI.setCount("2");
        yield return new WaitForSeconds(1);
        controladorDeUI.setCount("1");
        yield return new WaitForSeconds(1);
        controladorDeUI.setCount("Go!");
        yield return new WaitForSeconds(1);
        controladorDeUI.hideCount();

        cdObs.PuedeGenerarObstaculos = true;
        iniciarjuego = true;
        gameObject.SetActive(false);
    }

    public static void reiniciarJuego()
    {
        SceneManager.LoadScene("Escenario1");
    }

    public float distanceTravelled;
    public IEnumerator cambiarPosicionCamara(CambiosCamara cambio)
    {
        distanceTravelled = 0;
        cambiadoCamara = true;
        switch (cambio.tipo)
        {
            case CambiosCamara.TiposCambioCamara.CAMBIOPOSICIONYROTACION:
                if (!cambio.conAnimacion)
                {
                    posicionCamara.position = cambio.posicion;
                    posicionCamara.rotation = cambio.rotacion;
                }
                else
                {
                    while (posicionCamara.position != cambio.posicion)
                    {
                        posicionCamara.position = Vector3.Lerp(posicionCamara.position, cambio.posicion, .5f);
                        yield return null;
                    }
                    tiempoTranscurrido = 1.8f;
                }
                break;
            case CambiosCamara.TiposCambioCamara.SEGUIMIENTORUTA:
                if (cambio.path != null)
                {
                    posicionCamara.rotation = cambio.path.path.GetRotationAtDistance(distanceTravelled, cambio.endOfPathInstruction);
                    while (cambio.path.path.GetPoint(cambio.path.path.NumPoints - 1) != posicionCamara.position)
                    {
                        distanceTravelled += 30 * Time.deltaTime;
                        posicionCamara.position = cambio.path.path.GetPointAtDistance(distanceTravelled, cambio.endOfPathInstruction);
                        if (posicionCamara.position.x >= cambio.path.path.GetPoint(1).x)
                        {
                            posicionCamara.rotation = Quaternion.RotateTowards(posicionCamara.rotation, Quaternion.Euler(0, -90, 0), 5f);
                        }
                        yield return null;
                    }
                    tiempoTranscurrido = 1f;
                }
                break;
            case CambiosCamara.TiposCambioCamara.ASIGNARPADRE:
                if (cambio.padre != null)
                {
                    posicionCamara.parent = cambio.padre;
                    posicionCamara.localPosition = cambio.posicion;
                    posicionCamara.localRotation = cambio.rotacion;
                }
                break;
        }
        yield return null;
        cambiadoCamara = false;
    }
}

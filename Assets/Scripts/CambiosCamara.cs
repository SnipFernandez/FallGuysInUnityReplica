using PathCreation;
using System;
using UnityEngine;

public class CambiosCamara
{
    public enum TiposCambioCamara{
        CAMBIOPOSICIONYROTACION,
        SEGUIMIENTORUTA,
        ASIGNARPADRE
    };

    public TiposCambioCamara tipo { get; set; }
    public Vector3 posicion { get; set; }
    public Quaternion rotacion { get; set; }
    public PathCreator path { get; set; }
    public bool conAnimacion { get; set; }
    public EndOfPathInstruction endOfPathInstruction { get; set; }
    public Transform padre { get; set; }
}
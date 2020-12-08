using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeCaidas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.SendMessage("Eliminado");
    }
}

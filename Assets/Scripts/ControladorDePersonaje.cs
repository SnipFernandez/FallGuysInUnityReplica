using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDePersonaje : MonoBehaviour
{
    public Transform player;    

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, 10 * Time.deltaTime);
    }
}

using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeObstaculos : MonoBehaviour
{
    public List<GameObject> obstaculos = new List<GameObject>();
    public PathCreator pathCreator;
    public float TiempoParaGenerarObstaculo = 2;
    public bool PuedeGenerarObstaculos = false;
    public float TiempoTranscurrido;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PuedeGenerarObstaculos)
        {
            TiempoTranscurrido += Time.deltaTime;
            if (TiempoTranscurrido >= TiempoParaGenerarObstaculo)
            {
                TiempoTranscurrido = 0;
                GameObject gmo = Instantiate(obstaculos[Random.Range(0, obstaculos.Count - 1)]);
                gmo.GetComponent<PathFollower>().pathCreator = pathCreator;
                gmo.SetActive(true);
            }
        }
    }
}

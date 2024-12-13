using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    [SerializeField] private float anchoImagen;
    private Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float espacio = velocidad * Time.time;
        float resto = espacio % anchoImagen;
        transform.position = posicionInicial + resto * direccion;
    }
}

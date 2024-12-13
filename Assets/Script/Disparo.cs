using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Disparo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion;
    private ObjectPool<Disparo> mypool;
    private float timer;

    public ObjectPool<Disparo> Mypool { get => mypool; set => mypool = value; }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= 4) 
        {
            timer= 0;
            Mypool.Release(this);
        }
    }
}

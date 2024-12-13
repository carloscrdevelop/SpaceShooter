using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Disparo disparoPrefab;
    [SerializeField] private GameObject SpawnPoint1;
    [SerializeField] private int numeroDisparos;
    [SerializeField] private float ratioDisparo;
    private ObjectPool<Disparo> pool;
    private float vidas = 100f;
    private float temporizador = 0.5f;
    private int numeroBomba = 3;
    
    private void Awake()
    {
        pool = new ObjectPool<Disparo>(CrearDisparo, GetDisparo, ReleaseDisparo, DestroyDisparo);
    }
    private Disparo CrearDisparo()
    {
       Disparo disparoCopia=  Instantiate(disparoPrefab, SpawnPoint1.transform.position, Quaternion.identity);
        disparoCopia.Mypool = pool;
        
        return disparoCopia;
       
    }

    private void GetDisparo(Disparo disparo)
    {
        disparo.transform.position = transform.position;
        disparo.gameObject.SetActive(true);
    }

    private void ReleaseDisparo(Disparo disparo)
    {
        disparo.gameObject.SetActive(false);
    }

    private void DestroyDisparo(Disparo disparo)
    {
        Destroy(disparo.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();
        DelimitarMovimiento();
        Disparar();
        
    }
    void Movimiento()
    {
        float inputH = Input.GetAxisRaw("Horizontal");
        float inputV = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputH, inputV).normalized * velocidad * Time.deltaTime);
    }
    void DelimitarMovimiento()
    {
        float xClamped = Mathf.Clamp(transform.position.x, -8.4f, 8.4f);
        float yClamped = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(xClamped, yClamped, 0);
    }
    void Disparar()
    {
        temporizador += 1 * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && temporizador > ratioDisparo) 
        {
          
            for (int i = 0; i < numeroDisparos; i++)
            {
                pool.Get();
                temporizador = 0;
            }
            
            
        }
        if (Input.GetKey(KeyCode.LeftControl) && temporizador > ratioDisparo && numeroBomba > 0)
        {
            float gradosPorDisparo = 360 / numeroDisparos;
            for (float j = 0; j < 360; j++)
            {
                Disparo disparoCopia = pool.Get();
                disparoCopia.gameObject.SetActive(true);
                disparoCopia.transform.position = transform.position;
                disparoCopia.transform.eulerAngles = new Vector3(0f, 0f, j);
            }
            numeroBomba = numeroBomba-1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DisparoEnemigo") || collision.gameObject.CompareTag("Enemigo"))
        {
            vidas = vidas - 20;
            Destroy(collision.gameObject);
            if (vidas <=0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

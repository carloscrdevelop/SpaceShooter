using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private GameObject disparoPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject spawnPoint2;
    [SerializeField] private bool boss;
    [SerializeField] private int vidasBoss=50;
    
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnearDisparos());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-1, 0, 0) * velocidad * Time.deltaTime);
    }
    IEnumerator SpawnearDisparos()
    {
        while (true ==true)
        {
            Instantiate(disparoPrefab, spawnPoint.transform.position, Quaternion.identity);
            Instantiate(disparoPrefab, spawnPoint2.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DisparoJugador"))
        {
            if(boss && vidasBoss>0) 
            { 
                vidasBoss--;
            }
            else
            {

                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                
            }

           
        }
    }
}

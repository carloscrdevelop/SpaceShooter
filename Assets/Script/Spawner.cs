using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject asteroidePrefab;
    [SerializeField] private TextMeshProUGUI textoOleadas;
    [SerializeField] private Vector3 puntoAleatorio;
    private bool aparicionBoss=false;

    // Start is called before the first frame update
    void Start()
    {   
        StartCoroutine(SpawnearEnemigos());
        
        StartCoroutine(SpawnearAsteroides());

    }

    // Update is called once per frame
    void Update()
    {
        if(aparicionBoss==true)
        {
            puntoAleatorio = new Vector3(transform.position.x, 0, 0);
            Instantiate(bossPrefab, puntoAleatorio, Quaternion.identity);
            aparicionBoss = false;

        }
       
    }
    IEnumerator SpawnearEnemigos() 
    {
        
        for (int i= 0; i < 5; i++) //nivel
        {
            
            for (int j = 0; j < 3; j++)//oleada
            {
                textoOleadas.text = "Nivel " + (i+1) + "-" + "Oleada " + j;
                yield return new WaitForSeconds(2f);
                textoOleadas.text = "";
                for (int k = 0; k < 10; k++) //enemigos
                {
                    puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    Instantiate(enemigoPrefab, puntoAleatorio, Quaternion.identity);
                    //Instantiate(enemigoPrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);

                }
            }
           aparicionBoss=true;
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(3f);
    }
    IEnumerator SpawnearAsteroides()
    {


        for (int i = 0; i < 5; i++) 
        {

            for (int j = 0; j < 3; j++)
            {
               
                yield return new WaitForSeconds(2f);
              
                for (int k = 0; k < 10; k++)
                {
                    Vector3 puntoAleatorio = new Vector3(transform.position.x, Random.Range(-4.5f, 4.5f), 0);
                    Instantiate(asteroidePrefab, puntoAleatorio, Quaternion.identity);
                    yield return new WaitForSeconds(0.5f);

                }
            }
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(3f);

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    public GameObject windObject;
    public Quaternion windDirection;
    public Vector3 windSpawnRange;
    public int windDensity;
    private float windSpawnSpeed = 10; // Make sure this matches or is greater to the Wind particle duration time,
                                       // otherwise it will slowly eat up more and more memory. Reason being, it
                                       // will spawn the wind objects faster then they will get destroyed.

    private float elapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= windSpawnSpeed)
        { 
            elapsedTime = 0;
            Debug.Log("Wind Spawner Trigger.");
            for (int i = 0; i < windDensity; i++)
            {
                Debug.Log("Density Check " + i);
                //Instantiate(windObject);

                Vector3 RandVector3 = new Vector3(
                    Random.Range(-windSpawnRange.x, windSpawnRange.x),
                    Random.Range(10, windSpawnRange.y),
                    Random.Range(0, -windSpawnRange.z));
                GameObject windEffectObject = Instantiate(windObject, RandVector3, windDirection);
            }
        }
    }
}
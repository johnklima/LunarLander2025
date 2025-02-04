using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroidspawner : MonoBehaviour
{
    public GameObject[] meteorPrefabs; // Place to put your meteors/rock prefabs
    public Transform planetTransform;  // The transform of the Gameobject(Planet)
    public float spawnInterval = 2f;   // spawntime
    public float orbitRadiusMin = 20f; // Min orbit radius
    public float orbitRadiusMax = 60f; // Max orbit radius
    public float orbitSpeedMin = 0.1f; // Min orbit speed
    public float orbitSpeedMax = 1.5f; // Max orbit speed
    public float meteorLifetime = 10f; //Time before meteor despawn

    private float timer;

    /* Create an empty GameObject, Attach the script to it, In the inspector drop your prefabs in the meteorprefab array,
      drop the planet in the planetTransform array, change the spwaning and orbiting as needed */

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Randomly selects one of your inserted prefabs
            int randomIndex = Random.Range(0, meteorPrefabs.Length);
            GameObject meteorPrefab = meteorPrefabs[randomIndex];

            
            float orbitRadius = Random.Range(orbitRadiusMin, orbitRadiusMax); //random radius the meteor orbits
            float orbitSpeed = Random.Range(orbitSpeedMin, orbitSpeedMax);    //random speed the meteor have

            //random spawn points inside of the orbitRadius Min-Max
            float angle = Random.Range(25f, 9 * Mathf.PI);
            Vector3 initialPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * orbitRadius;

            // this spawns the meteor
            GameObject meteor = Instantiate(meteorPrefab, initialPosition, Quaternion.identity);

            //Starts a co routine to orbiting and the destruction
            StartCoroutine(OrbitAndDestroy(meteor, orbitSpeed, orbitRadius));
            
           
        }
    }

    IEnumerator OrbitAndDestroy(GameObject meteor, float orbitSpeed, float orbitRadius)
    {
        float angle = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < meteorLifetime)

       
        {
            //the orbits position
            angle += orbitSpeed * Time.deltaTime;
            Vector3 orbitPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * orbitRadius;

            //Meteor position
            meteor.transform.position = orbitPosition;

            elapsedTime += Time.deltaTime;


            yield return null; 
        }
        //destroys the meteor
        Destroy(meteor);
    }


}

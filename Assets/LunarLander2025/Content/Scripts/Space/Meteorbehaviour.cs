using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorbehaviour : MonoBehaviour
{
    public GameObject[] meteorPrefabs; // Place to put your meteors/rock prefabs
    public Transform planetTransform;  // The transform of the Gameobject(Planet)
    public float spawnInterval = 2f;   // Time between spawns
    public float orbitRadiusMin = 5f;  // Min orbit radius
    public float orbitRadiusMax = 10f; // Max orbit radius
    public float orbitSpeedMin = 1f;   // Min orbit speed
    public float orbitSpeedMax = 3f;   // Max orbit speed
    public float meteorSpeed = 5f;     // Speed of the meteor towards the planet

    private float timer;

    /* Create an empty GameObject, Attach the script to it, In the inspector drop your prefabs in the meteorprefab array,
      drop the planet in the planetTransform array, change the spwaning and orbiting as needed */

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Randomly selects a meteor/rock prefab
            int randomIndex = Random.Range(0, meteorPrefabs.Length);
            GameObject meteorPrefab = meteorPrefabs[randomIndex];

            float orbitRadius = Random.Range(orbitRadiusMin, orbitRadiusMax); //random radius the meteor orbits
            float orbitSpeed = Random.Range(orbitSpeedMin, orbitSpeedMax);    //random speed the meteor have

            // random spawn points within the orbitRadius Min and Max range
            float angle = Random.Range(0f, 5 * Mathf.PI);
            Vector3 initialPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 1f, Mathf.Sin(angle)) * orbitRadius;

            //spawns the meteor
            GameObject meteor = Instantiate(meteorPrefab, initialPosition, Quaternion.identity); 

            //meteors going towards the planet
            Vector3 directionToPlanet = (planetTransform.position - meteor.transform.position).normalized;

            //Starts a co routine to orbiting and the destruction
            StartCoroutine(OrbitAndDestroy(meteor, orbitSpeed, orbitRadius, directionToPlanet));
        }
    }

    IEnumerator OrbitAndDestroy(GameObject meteor, float orbitSpeed, float orbitRadius, Vector3 directionToPlanet)
    {
        float angle = 0f;

        while (true)
        {
            //The orbits position
            angle += orbitSpeed * Time.deltaTime;
            Vector3 orbitPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * orbitRadius;

            //Meteors position towards the planet
            meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, planetTransform.position, meteorSpeed * Time.deltaTime);

            //Meteor gets destroyed on contact with planet
            if (Vector3.Distance(meteor.transform.position, planetTransform.position) < planetTransform.localScale.x / 2f)
            {
                Destroy(meteor);
                yield break; 
            }

            yield return null; 
        }
    }
}
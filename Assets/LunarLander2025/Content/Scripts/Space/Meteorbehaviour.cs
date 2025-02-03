using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorbehaviour : MonoBehaviour
{
    public GameObject[] meteorPrefabs; 
    public Transform planetTransform; 
    public float spawnInterval = 2f; 
    public float orbitRadiusMin = 5f; 
    public float orbitRadiusMax = 10f; 
    public float orbitSpeedMin = 1f; 
    public float orbitSpeedMax = 3f; 
    public float meteorSpeed = 5f; // Speed of the meteor towards the planet

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Randomly selects a meteor/rock prefab
            int randomIndex = Random.Range(0, meteorPrefabs.Length);
            GameObject meteorPrefab = meteorPrefabs[randomIndex];

            float orbitRadius = Random.Range(orbitRadiusMin, orbitRadiusMax);
            float orbitSpeed = Random.Range(orbitSpeedMin, orbitSpeedMax);

            // random spawn points within the orbitRadius Min and Max range
            float angle = Random.Range(0f, 5 * Mathf.PI);
            Vector3 initialPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 1f, Mathf.Sin(angle)) * orbitRadius;

            
            GameObject meteor = Instantiate(meteorPrefab, initialPosition, Quaternion.identity); //spawns the meteor

            
            Vector3 directionToPlanet = (planetTransform.position - meteor.transform.position).normalized;//meteors going towards the planet

            
            StartCoroutine(OrbitAndDestroy(meteor, orbitSpeed, orbitRadius, directionToPlanet));
        }
    }

    IEnumerator OrbitAndDestroy(GameObject meteor, float orbitSpeed, float orbitRadius, Vector3 directionToPlanet)
    {
        float angle = 0f;

        while (true)
        {
            
            angle += orbitSpeed * Time.deltaTime;
            Vector3 orbitPosition = planetTransform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * orbitRadius;

            
            meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, planetTransform.position, meteorSpeed * Time.deltaTime);

            
            if (Vector3.Distance(meteor.transform.position, planetTransform.position) < planetTransform.localScale.x / 2f)
            {
                Destroy(meteor);
                yield break; 
            }

            yield return null; 
        }
    }
}
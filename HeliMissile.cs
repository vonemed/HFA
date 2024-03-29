﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeliMissile : MonoBehaviour
{
    // Missile parameters
    private float speed = 3f;

    // Explosion prefab
    public GameObject _explosion;
    private GameObject _explosionInst;

    // Target in front of heli
    public Transform terrainTarget;

    // UI
    public Text score; // To display score
    private int count; // To count points

    private void Start()
    {
        // Set score variable to an already existing Text object
        score = GameObject.Find("Score").GetComponent<Text>(); 
        // Convert written string into an int
        int.TryParse(score.text, out count);
        // Convert int into a string
        SetCountText();

        terrainTarget = GameObject.Find("terrainTarget").transform;
    }

    void OnTriggerEnter(Collider other) // On collision
    {
        if(other.CompareTag("Tank")) // If the missile hits tank
        {
            count += 200; // Add 200 points
            SetCountText(); // Refresh score on the screen

        } else if (other.CompareTag("Truck")) // If the missile hits truck
        {
            count += 100; // Add 100 points
            SetCountText(); // Refresh score on the screen

        } else if (other.CompareTag("Turret")) // If the missile hits turret
        {
            count += 400; // Add 400 points
            SetCountText(); // Refresh score on the screen

        } 

        FindObjectOfType<AudioManager>().Play("Explosion"); // Play explosion sound
        ObjectPooler.Instance.SpawnFromPool("Explosion", transform.position, transform.rotation); // Create an explosion
        StartCoroutine(destroyAfterTime());


    }

    public void Update()
    {
        if (gameObject.activeSelf == true)
        {
            float movPerFrame = speed * Time.deltaTime; // The speed of missile
            Vector3 dir = terrainTarget.position - transform.position; // The direction of missile
            transform.Translate(dir * movPerFrame, Space.World); // The actual movement
        } 
    }

    public void SetTarget(Transform _target)
    {
        terrainTarget = _target;
    }

    void SetCountText()
    {
        score.text = count.ToString(); // Convert int to string
    }

    IEnumerator destroyAfterTime()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(1.5f);
        ObjectPooler.Instance.Deactivate("Explosion");
        gameObject.SetActive(false); // Diactivate the missile
    }
}

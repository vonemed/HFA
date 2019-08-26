using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeliMissile : MonoBehaviour
{
    // Missile parameters
    private float speed = 5f;

    // Explosion prefab
    public GameObject _explosion;
    public GameObject _explosionInst;

    // Target in front of heli
    private Transform terrainTarget;

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
    }

    void OnTriggerEnter(Collider other) // On collision
    {
        if(other.CompareTag("Tank")) // If the missile hits tank
        {
            count += 200; // Add 200 points
            SetCountText(); // Refresh score on screen

        } else if (other.CompareTag("Truck")) // If the missile hits truck
        {
            count += 100; // Add 100 points
            SetCountText(); // Refresh score on screen
        } 

       _explosionInst = Instantiate(_explosion, transform.position, transform.rotation); // Create a particle explosion
        Destroy(gameObject);
        /*Destroy(gameObject.GetComponent<Rigidbody>()); // Remove rigidbody from missile, so it won't fall underground.
        gameObject.transform.position = new Vector3(-20, 2, 1); // Set it to the default spawn position*/
    }

    private void Update()
    {
        float movPerFrame = speed * Time.deltaTime; // The speed of missile
        Vector3 dir = terrainTarget.position - transform.position; // The direction of missile
        transform.Translate(dir * movPerFrame, Space.World); // The actual movement
    }

    public void SetTarget(Transform _target)
    {
        terrainTarget = _target;
    }

    void SetCountText()
    {
        score.text = count.ToString(); // Convert int to string
    }
}

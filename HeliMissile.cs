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

    // UI
    public Text _score; // To display score
    private int _count; // To count points

    // Target
    private Transform terrainTarget;

    public void SetTarget(Transform _target)
    {
        terrainTarget = _target;
    }

    private void Start()
    {
        _count = 0;
        SetCountText();
        _score.text = "";
    }
    
    void OnTriggerEnter(Collider other) // On collision
    {
        if(other.CompareTag("Tank")) // If the missile hits tank
        {
            _count += 200; // Add 200 points
            SetCountText(); // Refresh score on screen

        } else if (other.CompareTag("Truck")) // If the missile hits truck
        {
            _count += 100; // Add 100 points
            SetCountText(); // Refresh score on screen
        } 

       _explosionInst = Instantiate(_explosion, transform.position, transform.rotation); // Create a particle explosion
        Destroy(gameObject);
        /*Destroy(gameObject.GetComponent<Rigidbody>()); // Remove rigidbody from missile, so it won't fall underground.
        gameObject.transform.position = new Vector3(-20, 2, 1); // Set it to the default spawn position*/
    }

    void SetCountText()
    {
        _score.text = _count.ToString(); // Convert int to string
    }

    private void Update()
    {
        float movPerFrame = speed * Time.deltaTime; // The speed of missile
        Vector3 dir = terrainTarget.position - transform.position; // The direction of missile
        transform.Translate(dir * movPerFrame, Space.World); // The actual movement
    }
}

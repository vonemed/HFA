using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyOnCol : MonoBehaviour
{

    public GameObject _explosion;
    public GameObject _explosionInst;
    public Text _score; // To display score

    private int _count; // To count points

    private void Start()
    {
        _count = 0;
        SetCountText();
        _score.text = "";
    }

    void OnTriggerEnter(Collider other) // On collision
    {
        if(other.gameObject.CompareTag("Tank")) // If the missile hits tank
        {
            _count += 200; // Add 200 points
            SetCountText(); // Refresh score on screen

        } else if (other.gameObject.CompareTag("Truck")) // If the missile hits truck
        {
            _count += 100; // Add 100 points
            SetCountText(); // Refresh score on screen
        }

       _explosionInst = Instantiate(_explosion, transform.position, transform.rotation); // Create a particle explosion

        Destroy(gameObject.GetComponent<Rigidbody>()); // Remove rigidbody from missile, so it won't fall underground.
        gameObject.transform.position = new Vector3(-20, 2, 1); // Set it to the default spawn position
        //Destroy(gameObject); //destroy the missile

    }

    void SetCountText()
    {
        _score.text = _count.ToString(); // Convert int to string
    }

}

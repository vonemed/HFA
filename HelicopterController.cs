using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
    public bool isFlat = true;
    // Missile prefab
    [Header("Missile parameters")]
    public GameObject missilePrefab;
    private GameObject missileInst;

    public Transform _spawnPos; // Spawn position of the missile 
    public Transform terrainTarget; // The direction towards the ground for missile to follow

    
    [Header("UI")]
    // UI
    public Button fireButton; // A button to instantiate a missile 
    public RawImage firstStar; // The image of first star to indicate the completion of first mission.
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Case"))  // If heli is colliding with a case
        {
            Destroy(other);
            Debug.Log("You collected the intel"); // Debug purposes

            // Update the mission screen
            firstStar.enabled = true;

        } 
    }

    private void Start()
    {
        fireButton.onClick.AddListener(InstMissile);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilt = Input.acceleration;
        if (isFlat)
        {
            //tilt = Quaternion.Euler(90, 0, 0) * tilt;
        }

        transform.Translate(0, 0, tilt.x * Time.deltaTime * 9);
    }

    public void InstMissile()
    {
        GameObject missileInst = (GameObject)Instantiate(missilePrefab, _spawnPos.position, _spawnPos.rotation);
        HeliMissile missile = missileInst.GetComponent<HeliMissile>();

        if(missile != null)
        {
            missile.SetTarget(terrainTarget);
        }
    }
}
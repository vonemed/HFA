using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
    private float speed = 9f;

    // Missile prefab
    [Header("Missile parameters")]
    public GameObject missilePrefab;
    private GameObject missileInst;
    public Transform _spawnPos; // Spawn position of the missile 
    public Transform terrainTarget; // The direction towards the ground for missile to follow

    public float fireRate = 1f;
    private float restBetweenShots = 0f;

    // UI
    [Header("UI")]
    public Button fireButton; // A button to instantiate a missile 
    public Scrollbar scrlBar; // A scrollbar to control helicopters height
    public RawImage firstStar; // The image of first star to indicate the completion of first mission.
    public Canvas missionCompleted; // A canvas that will be enabled when player deliver case to the base
    public Canvas gameUI;
  
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Case"))  // If heli is colliding with a case
        {
            Destroy(other.gameObject);
            Debug.Log("You collected the intel"); // Debug purposes

            // Update the mission screen
            firstStar.enabled = true;

        } else if (other.CompareTag("Base"))
        {
            if(firstStar.enabled == true) // If mission is completed disable main game ui and enable "mission complete" panel
            {
                missionCompleted.enabled = true;
                gameUI.enabled = false;
            }
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
        Debug.Log(tilt);
        
        transform.Translate(0, 0, tilt.x * (speed * Time.deltaTime), Space.Self);

        restBetweenShots -= Time.deltaTime; // Cooldown reset
    }

    public void InstMissile()
    {
        if (restBetweenShots <= 0)
        {
            GameObject missileInst = (GameObject)Instantiate(missilePrefab, _spawnPos.position, _spawnPos.rotation);
            HeliMissile missile = missileInst.GetComponent<HeliMissile>();

            if (missile != null)
            {
                missile.SetTarget(terrainTarget);
            }

            restBetweenShots = 2f / fireRate; // Cooldown set
        }
    }
}
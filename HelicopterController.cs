using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
    // Helicopter parameters
    [Header("Helicopter parameters")]
    public float Xspeed;
    public float Yspeed;

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
        restBetweenShots -= Time.deltaTime; // Cooldown reset
        
        // Helicopter x axis movement
        Vector3 tilt = Input.acceleration;

        transform.Translate(0, 0, tilt.x * (Xspeed * Time.deltaTime), Space.Self);

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, 0, 4f * Time.deltaTime);

        } else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(0, 0, -4f * Time.deltaTime);
        }

        // Helicopter y axis movement
        // If scrollbar handle is above half, the heli goes up and if handle is less than half
        if(scrlBar.value > 0.5f) // Up
        {
            if(scrlBar.value > 0.8f) // To speed up lifting speed
            {
                transform.Translate(0, Yspeed * (0.5f * Time.deltaTime), 0);
            }

            transform.Translate(0, Yspeed * Time.deltaTime, 0);

        } else if (scrlBar.value < 0.5f) // Down
        {
            if(scrlBar.value < 0.2f) 
            { 
                transform.Translate(0, Yspeed * (-0.5f * Time.deltaTime), 0);
            }

            transform.Translate(0, Yspeed * (-Time.deltaTime), 0);

        } else if (scrlBar.value == 0.5f)
        {
            transform.Translate(0, 0, 0);
        }
    }

    public void InstMissile()
    {
        if(transform.position.y < 1.2f) // Destroy heli, if missile was launched too close to the ground
        {
            Destroy(gameObject);
        }
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
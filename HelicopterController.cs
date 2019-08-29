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

    private float minRotation = -45f;
    private float maxRotation = 45f;

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
        Vector3 currentRotation = transform.rotation.eulerAngles;


        currentRotation.x = Mathf.Clamp(tilt.x * 20f, minRotation, maxRotation); // Result = between min and max
        currentRotation.y = 90f;
        currentRotation.z = 0f;

        // Applies translation
        transform.Translate(0, 0, tilt.x * (Xspeed * Time.deltaTime), Space.Self);
        // Applies rotation
        transform.localRotation = Quaternion.Euler(currentRotation);
        
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
            if(scrlBar.value >= 0.8f) // To speed up lifting speed
            {
                if(scrlBar.value > 0.9f)
                {
                    transform.Translate(0, Yspeed * (1.2f * Time.deltaTime), 0); // Great increase in lifting speed
                }

                transform.Translate(0, Yspeed * (0.7f * Time.deltaTime), 0);
            }

            transform.Translate(0, Yspeed * Time.deltaTime, 0);

        } else if (scrlBar.value < 0.4f) // Down
        {
            if(scrlBar.value <= 0.2f)  // To speed up lifting speed
            { 
                if(scrlBar.value < 0.1f)
                {
                    transform.Translate(0, Yspeed * (-1.2f * Time.deltaTime), 0); // Great decrease in lifting speed
                }

                transform.Translate(0, Yspeed * (-0.7f * Time.deltaTime), 0);
            }

            transform.Translate(0, Yspeed * (-Time.deltaTime), 0);

        } else if (scrlBar.value < 0.5f && scrlBar.value > 0.4f)
        {
            transform.Translate(0, 0, 0);
            
        }
    }

    public void InstMissile()
    {
        if(transform.position.y <= 1.8f) // Destroy heli, if missile was launched too close to the ground
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
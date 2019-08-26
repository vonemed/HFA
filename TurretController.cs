using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private AxisData yAxisRot;
    [SerializeField]
    private AxisData xAxisRot;

    [SerializeField]
    [Range(0, 180f)]
    private float yAngleToTargetToStartXRotation;

    [SerializeField]
    private Transform target;

    public bool xPointingActive;
 
    [Header("Missile Attributes")] // A Header which displays in the unity editor

    public GameObject missilePrefab; // A missile prefab to instantiate when shooting
    public Transform launchingPoint; // A point (empty game object) from which the missile will be instantiated;

    public float fireRate = 1f; // The fire rate of missile
    private float restBetweenShots = 0f; // The cooldown between shots
    public bool Firing; // Flag

    [Header("UI")]
    public RawImage thirdStar;
    // Flag to indicate if the turret was destroyed
    private GameObject isTurretDestroyed; 

    private void Awake()
    {
        isTurretDestroyed = GameObject.FindGameObjectWithTag("Turret");
    }
    private void OnTriggerEnter(Collider other) // When heli is inside of turret sphere collider
    {
        if (other.CompareTag("Player_heli"))
        {
            Firing = true;
            //gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) // When heli is outside of turret sphere collider
    {
        Firing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurretDestroyed != null) // If the turret was already destroyed
        {
            if (restBetweenShots <= 0f && Firing)
            {
                missileLaunch();
                // Missiles per 3 seconds
                restBetweenShots = 3f / fireRate; // If fire rate is 2, that means turret need to fire 2 missiles per second
            }

            restBetweenShots -= Time.deltaTime;

            if (target != null)
            {
                float timeDelta = Time.deltaTime; // Smooth aming.
                yAxisRot.SetTarget(target);
                yAxisRot.Update(timeDelta, AimingAngle());


                xPointingActive = yAxisRot.getCurrentAbsAngle <= yAngleToTargetToStartXRotation; // [True if
                if (xPointingActive)
                {
                    float angle = Quaternion.LookRotation((target.position - xAxisRot.rotationTransform.position).normalized, xAxisRot.rotationTransform.up).eulerAngles.x;
                    angle = FixNegativeAngle(angle);
                    xAxisRot.rotationTransform.localRotation = Quaternion.Lerp(xAxisRot.rotationTransform.localRotation, Quaternion.Euler(xAxisRot.rotationMask * angle), xAxisRot.speed * timeDelta);
                }
                else
                {
                    xAxisRot.rotationTransform.localRotation = Quaternion.Lerp(xAxisRot.rotationTransform.localRotation, Quaternion.identity, xAxisRot.speed * timeDelta);
                }
            }

        } else
        {
            thirdStar.enabled = true; // Add third star when turret is destroyed
        }
    }
    private float FixNegativeAngle(float angle)
    {
        return angle > 0 ? angle : angle + 360f;
    }

    private float GetAngleBetween(Vector2 vector1, Vector2 vector2)
    {
        float dot = vector1.x * vector2.x + vector1.y * vector2.x;
        float det = vector1.x * vector2.y - vector1.y * vector2.x; // Tang?
        return Mathf.Rad2Deg * Mathf.Atan2(det, dot);
    }
    private float AimingAngle()
    {
        Vector3 targetPos = target.position;
        targetPos.y = targetPos.z;
        Vector3 turretPos = yAxisRot.rotationTransform.position;
        turretPos.y = turretPos.z;
        Vector3 forward = yAxisRot.rotationTransform.forward;
        return GetAngleBetween(forward, (targetPos - turretPos).normalized); // Angle between forward vector and vector to the target
    }

    void missileLaunch()
    {
        
        // Reference to a missile that instantiated, [Object casting]
        GameObject tempMissile = (GameObject)Instantiate(missilePrefab, launchingPoint.position, missilePrefab.transform.rotation);
        TurretMissile missile = tempMissile.GetComponent<TurretMissile>();

        if (missile != null) // If the component was found succesful
        {
            missile.TargetToFollow(target); // Pass the target transform
        }
        //missilePrefab.transform.Rotate(0, -90, 0); // Rotate the missile upon instatiation
    }
}
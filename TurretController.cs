using UnityEngine;

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

    public Transform missile;
    public bool Firing;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player_heli"))
        {
            missile.LookAt(other.transform);
            Firing = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Firing)
        {
            missile.Translate(0, 1.0f * Time.deltaTime, 1.0f * Time.deltaTime);
        }
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


}
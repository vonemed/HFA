using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // To display parameters in Unity
public class AxisData
{
    public Transform rotationTransform;
    public float speed;

    public Vector3 rotationMask; // To avoid mix up with float and vec3 in rotation(update) function

    private Transform target;
    private bool aiming;
    private float angle;

    public float getCurrentAbsAngle
    {
        get
        {
            return Abs(angle);
        }
    }
    private float Abs(float val)
    {
        return val < 0 ? -val : val;
    }
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        aiming = true;
    }
	// Update is called once per frame
	public void Update (float timeDelta, float angle)
    {
		if(aiming)
        {
            this.angle = angle;

            rotationTransform.Rotate(rotationMask * (speed * timeDelta * Mathf.Sign(angle)));
        }
	}
}

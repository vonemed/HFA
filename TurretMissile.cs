using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    private Transform target; // A target for missile to follow.
    public GameObject explosionEff; // Explosion effect when missile hit.

    public float speed = 5f; // The speed of missile.

    public void TargetToFollow (Transform _target)
    {
        target = _target;
    }

	// Update is called once per frame.
	void Update ()
    {
        if(target == null) // In case there is no target.
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position; // Direction vector to the target.
        float movPerFrame = speed * Time.deltaTime; // How fast missile moves per frame.

        if(dir.magnitude <= movPerFrame) // To prevent the missile from overshooting, and getting past the target.
        {
            Debug.Log("HIT");
            Destroy(gameObject);

            // Instantiating explosion effect
            GameObject tempEff = (GameObject)Instantiate(explosionEff, transform.position, transform.rotation);
            Destroy(tempEff, 2f); // Destroying explosion effect after 2 seconds.
            return;
        }

        transform.Translate(dir.normalized * movPerFrame, Space.World); // Actuall movement of missile.
	}
}

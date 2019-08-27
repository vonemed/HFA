using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : MonoBehaviour
{
    private Transform target; // A target for missile to follow.
    public GameObject explosionEff; // Explosion effect when missile hit.

    [Header("Missile parameters")]
    public float speed = 5f; // The speed of missile.
    private float delay = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_heli"))
        {
            Destroy(other.gameObject);
            GameObject tempEff = (GameObject)Instantiate(explosionEff, transform.position, transform.rotation);
            Destroy(tempEff, 2f); // Destroying explosion effect after 2 seconds.
        }
    }
    public void TargetToFollow(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame.
    void Update()
    {
        if (target == null) // In case there is no target.
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position; // Direction vector to the target.
        float movPerFrame = speed * Time.deltaTime; // How fast missile moves per frame.

        if (dir.magnitude <= movPerFrame) // To prevent the missile from overshooting, and getting past the target.
        {

            Destroy(gameObject);

            // Instantiating explosion effect
            GameObject tempEff = (GameObject)Instantiate(explosionEff, transform.position, transform.rotation);
            Destroy(tempEff, 2f); // Destroying explosion effect after 2 seconds.
            return;
        }

        transform.Translate(dir.normalized * movPerFrame, Space.World); // Actuall movement of missile.

        if (gameObject != null) // If missile is instantiated
        {
            StartCoroutine(DestroyAfterTime()); // Launch a coroutine to destroy missile after certain seconds
        }
    }
    IEnumerator DestroyAfterTime() // Destroy missile after some time
    {
        // Wait for 5 seconds, then destroy missile and instantiate an explosion
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        GameObject tempEff = (GameObject)Instantiate(explosionEff, transform.position, transform.rotation);
        Destroy(tempEff, 2f); // Destroying explosion effect after 2 seconds.
    }
}

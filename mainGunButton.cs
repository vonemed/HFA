using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mainGunButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ParticleSystem muzzleFlash;
    public Transform shootingPoint;
    public float weaponRange = 20f;

    private bool hold;

    public void  OnPointerDown(PointerEventData eventData)
    {
        hold = true;

        FindObjectOfType<AudioManager>().Play("MachineGun");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hold = false;

        FindObjectOfType<AudioManager>().Stop("MachineGun");
    }

    private void Update()
    {
        if (muzzleFlash != null)
        {
            if (hold == true)
            {
                muzzleFlash.Play();
                shootRay();
            }
            else
            {
                muzzleFlash.Stop();
            }
        }
    }

    void shootRay()
    {
        Vector3 rayOrigin = shootingPoint.position;
        RaycastHit hit;

        if(Physics.Raycast(rayOrigin, shootingPoint.forward, out hit, weaponRange))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.CompareTag("Tank"))
            {
                hit.transform.GetComponent<EnemyController>().health -= 1f;

            } else if (hit.transform.CompareTag("Truck"))
            {
                hit.transform.GetComponent<EnemyController>().health -= 5f;

            } else if (hit.transform.CompareTag("Turret_missile"))
            {
                Destroy(hit.transform.gameObject);
                hit.transform.GetComponent<TurretMissile>().InstAnExplosion();
            }
        }
    }
}

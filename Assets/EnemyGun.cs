using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float range;
    public float damage;
    public float fireaRate;
    public float impactForce;
    public GameObject impactEffect;

    public Transform firepoint;


    public void Shoot() 
    {
        // Debug.Log("shootgun 1");
        RaycastHit hitInfo;
        if (Physics.Raycast(firepoint.position, firepoint.forward, out hitInfo, range))
        {
            // Debug.Log(hitInfo.transform.name);
            Debug.Log("shootgun 2");

            PlayerMovement Player = hitInfo.transform.GetComponent<PlayerMovement>();

            if (Player != null)
            {   //it sends the amount of damage to the Target script for TakeDamage function
                Player.TakeDamage(damage);
            }
                    
            // Add force to the target when we hit it( moving the target when we shoot to it
            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }
            // Creating impact effect when we shot at some thing 
            GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactGO, 2f);
        }
        else
        {
            Debug.Log("Didn't hit anything");
        }
    }

}

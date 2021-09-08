using UnityEditor;
using UnityEngine;

//https://youtu.be/THnivyG0Mvo?list=PLPV2KyIb3jR7dFbE2UQYu7QWMdUgDnlnk
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject imapctEffect;
    public float impactForce = 30f;
    public Vector3 aimDownSight;
    public Vector3 hipFire;
    private float nextTimeToFire = 0f;
    public float aimSpeed;
    
    // Update is called once per frame
    void Update()
    {   
        //Gathering information from input system whenever pressing mouse left button and fireRate condition 
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        Aim();
            
    }
    
    
    void Shoot()
    {
        // applying muzzle flash
        muzzleFlash.Play();
            
        //
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            Target target = hitInfo.transform.GetComponent<Target>();

            if (target != null)
            {   //it sends the amount of damage to the Target script for TakeDamage function
                target.TakeDamage(damage);
            }
                    
            // Add force to the target when we hit it( moving the target when we shoot to it
            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            }
            // Creating impact effect when we shot at some thing 
            GameObject impactGO = Instantiate(imapctEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impactGO, 2f);
        }

            
    }

    void Aim()
    {
        if (Input.GetButton("Fire2"))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, aimDownSight,
                aimSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = hipFire;
        }
    }
}
// aimClose transform. position x= 0.01 y= -0.16  z= -0.17
// aimClose transform. rotation x=-1   y= 88.7  z= 0

// aimOpen transform. position X= .16   y= -0.22   z= 0.23
// aimOpen transform. rotation X= -1   y=88.7   z=0 






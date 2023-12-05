using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject ball;
    public GameObject dart;

    public int totalThrows;
    public float throwCooldown;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    private bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow)
        {
            Throw();
        }
    }

    private void Throw()
    {
        //instantiate throwable
        GameObject projectile = Instantiate(ball, attackPoint.position, cam.rotation);

        //get RB
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

        //calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        //use da force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

       projectileRB.AddForce(forceToAdd, ForceMode.Impulse);

        

        totalThrows--;

        //cooldown time
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
        
    }
}

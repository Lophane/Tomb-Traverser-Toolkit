using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowablesScript : MonoBehaviour
{

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow = false;

    private void Start()
    {
        Invoke("PreparingToThrow", 8f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0 && Time.timeScale == 1f)
        {
            Throw();
        }
    }

    private void Throw()
    {

        readyToThrow = false;

        //Create object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        //Get RB componenet
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f) && !hit.collider.isTrigger)
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        //Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    void PreparingToThrow()
    {
        readyToThrow = true;
    }

}

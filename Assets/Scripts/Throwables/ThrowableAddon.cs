using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAddon : MonoBehaviour
{

    public bool isSticky;
    public int damage;

    private Rigidbody rb;

    private bool targetHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (targetHit)
            return;
        else
            targetHit = true;

        if (collision.gameObject.GetComponent<BasicEnemy>() != null)
        {
            BasicEnemy enemy = collision.gameObject.GetComponent<BasicEnemy>();

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (isSticky)
        {
            rb.isKinematic = true;

            transform.SetParent(collision.transform);
        }
            
    }

}

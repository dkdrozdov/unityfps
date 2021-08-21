using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamageable
{
    public Health health;
    Rigidbody rootRigidbody;
    public void TakeDamage(float damage)
    {
        TakeDamage(damage, 0f, Vector3.zero);
    }
    public void TakeDamage(float damage, float punch, Vector3 source)
    {
        health.TakeDamage(damage);
        //Debug.DrawRay(source, (transform.position - source).normalized * Vector3.Distance(source, transform.position) * 2, Color.red, 30f);
        RaycastHit[] raycastHits = Physics.RaycastAll(source, transform.position - source, Vector3.Distance(source, transform.position) * 2);
        foreach (RaycastHit raycasthit in raycastHits)
        {
            if (raycasthit.transform.root == transform.root)
            {
                rootRigidbody.AddForceAtPosition((transform.position - source).normalized * punch, raycasthit.point, ForceMode.Impulse);
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rootRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

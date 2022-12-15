using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _projectileSpeed;
    public Rigidbody _rb;
    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        Vector3 moveDirection = transform.forward * _projectileSpeed;
        _rb.AddForce(moveDirection);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject.GetComponent<GameObject>();
        if(other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(1);
            Debug.Log(other + "damage taken");
        }
        Destroy(gameObject);
    }
}

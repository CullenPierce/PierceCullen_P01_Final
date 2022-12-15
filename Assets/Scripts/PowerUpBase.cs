using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract void PowerDown(Player player);

    [SerializeField] float _powerupDuration = 3;

    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    Player player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }
    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            Feedback();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            //visual.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            StartCoroutine(PowerUpDuration(_powerupDuration));
            Debug.Log("Test this");
            
            
        }
    }
    private void Feedback()
    {
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
    IEnumerator PowerUpDuration(float powerupDuration)
    {
        Debug.Log("Duration Start");
        yield return new WaitForSeconds(powerupDuration);
        Debug.Log("Test number 2");
        PowerDown(player);
        gameObject.SetActive(false);
    }
}

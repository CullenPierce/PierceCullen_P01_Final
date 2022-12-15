using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _spawnPoint;

    [SerializeField] float projectileFrequency = 2f;

    [SerializeField] Transform playerLocation;
    [SerializeField] int _health = 10;
    [SerializeField] float _bossSpeed = .1f;

    [SerializeField] int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    [SerializeField] Text _bossHealthTextUI = null;

    Rigidbody _rb = null;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        InvokeRepeating("Shoot", 2.0f, projectileFrequency);
    }
    public void TakeDamage(int damage)
    {
        _health--;
        _bossHealthTextUI.text = "Boss Health: " + _health;
        Debug.Log(_health);
        if(_health <= 0)
        {
            Kill();
        }
    }
    private void Kill()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.LookAt(playerLocation);
        Vector3 moveDirection = transform.forward * _bossSpeed;
        _rb.AddForce(moveDirection);
    }
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }
    protected virtual void PlayerImpact(Player player)
    {
        player.DecreaseHealth(_damageAmount);
    }

    private void ImpactFeedback()
    {
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);

        }
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }
    public void Shoot()
    {
        Instantiate(_projectile, _spawnPoint.position, _spawnPoint.rotation);
        Debug.Log(_spawnPoint.rotation);
    }

}

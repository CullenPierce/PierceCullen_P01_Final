using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Text _treasureAmountTextUI = null;
    [SerializeField] int _maxHealth = 3;
    int _currentHealth;

    int _currentTreasure;

    public bool _damageable = true;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
        _treasureAmountTextUI.text = "Player Health: " + _currentHealth;
    }
    

    public void DecreaseHealth(int amount)
    {
        if(_damageable == true)
        {
            _currentHealth -= amount;
            Debug.Log("Player's health: " + _currentHealth);
            _treasureAmountTextUI.text = "Player Health: " + _currentHealth;
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
        
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void IncreaseTreasure(int amount)
    {
        _currentTreasure += amount;
        _treasureAmountTextUI.text = "Treasure: " + _currentTreasure;
        Debug.Log("Treasure " + _currentTreasure);
    }

    public void TakeDamage(int damage)
    {
        DecreaseHealth(1);
    }
}

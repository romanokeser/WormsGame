using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WormHealth : MonoBehaviour
{

    private int _health;
    public int MaxHealth = 100;

    [SerializeField] private TMP_Text _healthText;

    // Start is called before the first frame update
    void Start()
    {
        _health = MaxHealth;
        _healthText.SetText(_health.ToString());
    }

    internal void ChangeHealth(int change)
    {
        _health += change;
        if (_health > MaxHealth)
            _health = MaxHealth;
        else if (_health <= 0)
            _health = 0;
        _healthText.SetText(_health.ToString());

    }
}

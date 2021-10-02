using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public int health { get { return _health; } set { DebugMessage(value); _health = value; } }
    private int _health;
    public float waterHydration;
    public float weight = 2.5f;
    public float growthSpeed = 1;
    private float growthSpeedMultyplier = 1;
    public bool test = false;

    private void Start()
    {
        if (test)
        {
            growthSpeed = 4;
            waterHydration = 5;
            health = 100;
            for (int i = 0; i < 11; i++)
            {
                GrowthPumkin();
            }
        }
        health = 100;
    }
    private void DebugMessage(int currentHealth)
    {
        UIDamage.Create(transform.position, currentHealth.ToString());
    }
    public void RestoreHealth(int healthToRestore)
    {
        if (_health < 100)
            health += healthToRestore;
        else if (_health + healthToRestore > 100 && _health != 100)
            health = 100;
        else
            Debug.Log("Невозможно восстановить здоровье");
    }
    public void IncreaseGrowthSpeed(float increaseValue)
    {
        growthSpeed += increaseValue;
    }
    public void GrowthPumkin()
    {
        growthSpeedMultyplier = health / 100;

        weight += (growthSpeed * growthSpeedMultyplier + (waterHydration / 10)) * 10;
        float scaleMultiplyer = weight / 100;
        weight = (float)Math.Round(weight, 2);
        waterHydration *= 0.3f;
        if (transform.localScale.x + scaleMultiplyer < 4.0f)
            transform.localScale = new Vector3(transform.localScale.x + scaleMultiplyer, transform.localScale.y + scaleMultiplyer, transform.localScale.y + scaleMultiplyer);
        Debug.Log("weight: " + weight);


    }
}

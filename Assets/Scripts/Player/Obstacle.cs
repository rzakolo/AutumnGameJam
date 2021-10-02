using System;
using UnityEngine;


internal class Obstacle : NonPlayerCharacter
{
    public int health { get { return _health; } set { ShowMessage(_health - value); _health = value; } }
    private int _health = 40;

    private void ShowMessage(int currentHealth)
    {
        UIDamage.Create(transform.position, currentHealth.ToString());
    }
    private void Start()
    {
        text = $"Press <color=#ffea00>E</color> to deinstall <b>fence</b>";
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void Action()
    {
        gameManager.fence++;
        Destroy(gameObject);
    }
}
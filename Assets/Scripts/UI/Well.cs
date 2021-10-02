using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Well : NonPlayerCharacter
{
    public override void Action()
    {
        if (gameManager.water != 10 && gameManager.shootgunOnHand)
        {
            gameManager.FillWateringCan();
        }
        else
            IncorrectAction();
    }
    private void Update()
    {
        string temp = gameManager.water != 10 ? $"{Mathf.RoundToInt(gameManager.water)}0%" : "Full";
        text = $"Press <color=#ffea00>E</color> to fill <b>watering can (current water: {temp})</b>";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class UIPumpkin : NonPlayerCharacter
{
    Pumpkin pumpkin;
    [SerializeField] Image pumpkinHealthImage;
    [SerializeField] Image waterHydrationImage;
    [SerializeField] Image growSpeedImage;

    public override void Action()
    {
        if (gameManager.fertilizer >= 1)
        {
            pumpkin.IncreaseGrowthSpeed(0.5f);
            gameManager.fertilizer--;
            text = $"Press <color=#ffea00>E</color> to use <b>fertilizer (You have {gameManager.fertilizer})</b>\nweight: {gameManager.pumpkin.weight}kg";
        }
        else
            IncorrectAction();
    }
    private void Update()
    {
        text = $"Press <color=#ffea00>E</color> to use <b>fertilizer (You have {gameManager.fertilizer})</b>\nweight: {gameManager.pumpkin.weight}kg";
    }
    void Start()
    {
        text = $"Press <color=#ffea00>E</color> to use <b>fertilizer (You have {gameManager.fertilizer})</b>";
        pumpkin = gameObject.GetComponent<Pumpkin>();
    }
}

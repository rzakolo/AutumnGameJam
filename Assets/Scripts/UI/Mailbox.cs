using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Mailbox : NonPlayerCharacter
{
    [SerializeField] GameObject fencyPrefab;
    private Vector3 offset = new Vector3(-0.4f, -0.2f, 2.4f);

    public override void Action()
    {
        if (gameManager.BuyFence())
        {
            fencyPrefab.transform.position = transform.position + offset;
            Instantiate(fencyPrefab);
            text = $"Press <color=#ffea00>E</color> to buy <b>fence ({gameManager.requiredMoneyToBuyFence}$)</b>";
        }
        else
            IncorrectAction();
    }

    void Start()
    {
        text = $"Press <color=#ffea00>E</color> to buy <b>fence ({gameManager.requiredMoneyToBuyFence}$)</b>";
    }
}

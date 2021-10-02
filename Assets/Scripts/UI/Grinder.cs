using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Grinder : NonPlayerCharacter
{
    [SerializeField] GameObject fertilizerPrefab;
    Vector3 offset = new Vector3(0.9f, 0.2f, -1.6f);

    public override void Action()
    {
        if (gameManager.ExchangeEssence())
        {
            fertilizerPrefab.transform.position = transform.position + offset;
            Instantiate(fertilizerPrefab);
            text = $"Press <color=#ffea00>E</color> to use <b>grinder ({gameManager.essence}/{gameManager.requiredEssenceToExchange})</b>";
        }
        else
            IncorrectAction();

    }
    void Update()
    {
        text = $"Press <color=#ffea00>E</color> to use <b>grinder ({gameManager.essence}/{gameManager.requiredEssenceToExchange})</b>";
    }
}

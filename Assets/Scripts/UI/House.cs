using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class House : NonPlayerCharacter
{
    public override void Action()
    {
        gameManager.SkipDay();
    }

    // Start is called before the first frame update
    void Start()
    {
        text = $"Press <color=#ffea00>E</color> to <b>skip day</b>";
    }

    // Update is called once per frame
    void Update()
    {

    }
}

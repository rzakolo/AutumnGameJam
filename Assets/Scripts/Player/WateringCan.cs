using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    [SerializeField] ParticleSystem waterParticle;
    GameManager gameManager;
    bool usingCan;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (gameManager.water - Time.deltaTime > 0)
            {
                gameManager.water -= Time.deltaTime;
                float distance = Vector3.Distance(transform.position, gameManager.GetPumpkinPos().position);
                if (distance - gameManager.pumpkin.transform.localScale.x / 2 < 3 && gameManager.pumpkin.waterHydration + Time.deltaTime < 10)
                    gameManager.pumpkin.waterHydration += Time.deltaTime;
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            waterParticle.Play();
        }
        if (gameManager.water < 0.1)
        {
            waterParticle.Stop();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            waterParticle.Stop();
        }
    }
}

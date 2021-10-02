using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] float spinSpeed;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "essence(Clone)")
                gameManager.essence++;
            else if (gameObject.name == "zabor2(Clone)")
                gameManager.fence++;
            else
                gameManager.fertilizer++;
            Destroy(gameObject);
        }
    }
}

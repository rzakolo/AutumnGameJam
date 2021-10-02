using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody projectileRb;
    GameManager gameManager;
    bool onTrigger = true;
    int damage;
    [SerializeField] ParticleSystem particle;
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void FixedUpdate()
    {
        projectileRb.AddRelativeForce(Vector3.forward * Time.fixedDeltaTime * 5, ForceMode.Impulse);
        if (transform.position.magnitude > 50)
        {
            Destroy(gameObject);
        }
    }
    public void SetDamage(int i)
    {
        damage = i;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pumpkin") && onTrigger)
        {
            gameManager.pumpkin.health -= damage;
            onTrigger = false;
            if (particle != null)
            {
                Instantiate(particle).transform.position = transform.position;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player") && onTrigger)
        {
            gameManager.player.health -= damage;
            onTrigger = false;
            if (particle != null)
            {
                Instantiate(particle).transform.position = transform.position;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Obstacle") && onTrigger)
        {
            other.GetComponent<Obstacle>().health -= damage;
            onTrigger = false;
            if (particle != null)
            {
                Instantiate(particle).transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootgun : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField] ParticleSystem shootParticle;
    float fireRate = 1;
    float _fireRate = 1;
    bool shoot;
    float firePointAngle = 4;
    private int damage = 5;
    Dictionary<EnemyBase, int> dictionary = new Dictionary<EnemyBase, int>();
    [SerializeField] float maxBulletDistance = 50;


    int raysNumber = 5;
    RaycastHit raycast;
    public LayerMask layerMask;
    public bool drawRays = false;

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !shoot)
        {
            Shoot();
            fireRate = _fireRate;
            shoot = true;
        }
        if (shoot)
        {
            fireRate -= Time.deltaTime;
            if (fireRate < 0)
                shoot = false;
        }

    }
    private void Shoot()
    {
        for (int i = 0; i < raysNumber; i++)
        {
            firePoint.localRotation = Quaternion.identity;
            firePoint.localRotation = Quaternion.Euler(firePoint.localRotation.x + RandomNumber(), firePoint.localRotation.y + RandomNumber(), firePoint.localRotation.z + RandomNumber());
            Vector3 forward = firePoint.TransformDirection(Vector3.forward);
            if (Physics.Raycast(firePoint.position, forward, out raycast, maxBulletDistance, layerMask))
            {
                if (raycast.collider != null)
                {
                    //raycast.collider.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
                    GetDamage(raycast.collider.gameObject.GetComponent<EnemyBase>());
                }
            }
            if (drawRays)
            {
                Debug.DrawRay(firePoint.position, forward * maxBulletDistance, Color.green, 5);
                //Debug.Log((forward * maxBulletDistance).magnitude);
            }
        }
        ShowDamage();
        ShootParticle();
    }
    private void GetDamage(EnemyBase enemy)
    {
        if (!dictionary.ContainsKey(enemy))
        {
            dictionary.Add(enemy, damage);
        }
        else
        {
            dictionary[enemy] += damage;
        }
    }
    private void ShowDamage()
    {
        foreach (var item in dictionary)
        {
            Vector3 forceDirection = (item.Key.transform.position - firePoint.position).normalized * 0.5f;
            item.Key.TakeDamage(item.Value, forceDirection);
        }
        dictionary = new Dictionary<EnemyBase, int>();
    }
    private void ShootParticle()
    {
        shootParticle.transform.position = firePoint.position;
        shootParticle.transform.rotation = firePoint.rotation;
        Instantiate(shootParticle);
    }
    private float RandomNumber()
    {
        return Random.Range(-firePointAngle, firePointAngle);
    }
}

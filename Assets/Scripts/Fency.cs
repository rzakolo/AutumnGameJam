using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fency : MonoBehaviour
{
    Vector3 mousePosition;
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject fenceForPlace;
    GameManager gameManager;
    float spinSpeed = 50;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        fencePrefab = Instantiate(fencePrefab);
        fencePrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            VisibleFence();
        }
        if (Input.GetKey(KeyCode.R))
        {
            RotateFence();
        }
        if (fencePrefab.activeInHierarchy && Input.GetButtonDown("Fire1"))
        {
            PlaceFence();
        }
    }

    void RotateFence()
    {
        fencePrefab.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
    void VisibleFence()
    {
        fencePrefab.SetActive(!fencePrefab.activeInHierarchy);
    }
    void UpdateMousePosition()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            mousePosition = ray.GetPoint(hitdist);
            mousePosition.y += 0.7f;
            fencePrefab.transform.position = mousePosition;
        }
    }
    void PlaceFence()
    {
        fencePrefab.SetActive(false);
        if (gameManager.fence >= 1)
        {
            gameManager.fence--;
            fenceForPlace.transform.position = fencePrefab.transform.position;
            fenceForPlace.transform.rotation = fencePrefab.transform.rotation;
            Instantiate(fenceForPlace).SetActive(true);
        }
    }
}

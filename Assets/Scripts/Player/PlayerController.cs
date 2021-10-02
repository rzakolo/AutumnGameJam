using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    [SerializeField] float speed;
    [SerializeField] float smoothRotationSpeed;
    [SerializeField] CharacterController playerController;
    [SerializeField] LayerMask NPCLayerMask;
    [SerializeField] GameManager gameManager;
    [SerializeField] Shootgun shootgun;
    [SerializeField] WateringCan wateringCan;
    public int health { get { return _health; } set { DebugMessage(_health - value); _health = value; } }
    private int _health = 100;
    Animator animator;
    float forwardAmount;
    float turnAmount;
    public Vector3 fireDirection;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InteractWithNPC();
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            shootgun.enabled = gameManager.shootgunOnHand;
            shootgun.gameObject.SetActive(gameManager.shootgunOnHand);
            gameManager.shootgunOnHand = !gameManager.shootgunOnHand;
            wateringCan.enabled = gameManager.shootgunOnHand;
            wateringCan.gameObject.SetActive(gameManager.shootgunOnHand);

        }
        if (transform.position.y < -1 && !gameManager.isGameOver)
            health = 0;
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        //movement += transform.forward * verticalInput;
        //movement += transform.right * horizontalInput;
        if (movement.magnitude >= 0.1f)
        {
            animator.SetFloat("Speed", movement.magnitude);
            playerController.Move(speed * Time.deltaTime * movement);
        }
        animator.SetFloat("Speed", movement.magnitude);
        playerController.Move(Physics.gravity * Time.deltaTime);
        RotateToMouse();
        ConvertMoveInput(movement);
        UpdateAnimator();
    }
    void RotateToMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Vector3 movement = targetPoint - transform.position;
            fireDirection = movement;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
        }
    }
    void InteractWithNPC()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, 5.5f, NPCLayerMask))
        {
            if (hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    npc.Action();
                }

            }

        }
        Debug.DrawRay(transform.position + Vector3.up, Vector3.forward * 5.5f, Color.green, 5);
    }
    private void DebugMessage(int currentHealth)
    {
        UIDamage.Create(transform.position, currentHealth.ToString());
    }
    void ConvertMoveInput(Vector3 movement)
    {
        Vector3 localMove = transform.InverseTransformDirection(movement);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }
    void UpdateAnimator()
    {
        animator.SetFloat("Look Y", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Look X", turnAmount, 0.1f, Time.deltaTime);
    }
}

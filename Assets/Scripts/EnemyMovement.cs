using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    bool grounded;
    private float NPCHeight;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;

    public Transform orientation;
    private bool canMove = false;
    public Transform playerTarget;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        playerTarget = GameObject.Find("PlayerCharacter").transform;
        NPCHeight = transform.localScale.y * 2;
        Invoke("StartGame", 2.5f);
        rb.freezeRotation = true;
    }

    private void Update()
    {

        Vector3 playerPosition = new Vector3(playerTarget.transform.position.x, 0f, playerTarget.transform.position.z);
        Vector3 NPCPosition = new Vector3(transform.position.x, 0f, transform.position.z);

        Vector3 direction = playerPosition - NPCPosition;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);


        if (Vector3.Distance(transform.position, playerTarget.position) <= 3f)
        {
            canMove = false;
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) >= 30f)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
        }

    }


    void StartGame()
    {
        canMove = true;
    }

}

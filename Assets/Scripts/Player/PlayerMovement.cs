using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Header("Settings")]

    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turningRate = 30f;

    private Vector2 previousMovementInput;


    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;

        inputReader.MoveEvent += HandleMove;
    }

   

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;

        inputReader.MoveEvent -= HandleMove;
    } 

    void Update()
    {
        if(!IsOwner) return;

        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
        
    }

    void FixedUpdate()
    {
        if(!IsOwner) return;

        rb.linearVelocity = movementSpeed * previousMovementInput.y * (Vector2) bodyTransform.up;
        
    }
     private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
}

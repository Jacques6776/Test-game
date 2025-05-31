using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    //Player movement controlls

    [SerializeField]
    private float baseMovemetSpeed = 5f;    
    private float currentMovementSpeed;
    private Rigidbody playerRB;
    private Vector2 moveVector;

    //Boost Controlls
    [SerializeField]
    private float boostedMovementSpeed = 10f;    
    private bool inBoostState = false;
    private bool boostInCooldown = true;
    [SerializeField]
    private bool boostAvailable = false;

    [SerializeField]
    private float maxBoostTime = 5f;
    private float boostTimer;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayer();

        if (inBoostState)
        {
            BoostPlayerMovement();
        }

        if (boostInCooldown)
        {
            ResetPlayerBoost();
        }
    }

    //Player movement methods
    public void Move(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(moveVector.x, 0f, moveVector.y);

        playerRB.linearVelocity = movement * currentMovementSpeed;

        Vector3 direction = transform.position + movement;
        transform.LookAt(direction);
    }

    //Player boost methods
    public void BoostState(InputAction.CallbackContext context)
    {
        if(context.performed && boostAvailable)
        {
            inBoostState = true;
        }
    }

    private void BoostPlayerMovement()
    {
        if(boostTimer > 0f)
        {
            currentMovementSpeed = boostedMovementSpeed;
            boostAvailable = false;
            inBoostState = true;

            boostTimer -= Time.deltaTime;
        }
        else if(boostTimer <= 0f)
        {
            inBoostState = false;
            boostInCooldown = true;
        }
    }

    private void ResetPlayerBoost()
    {
        currentMovementSpeed = baseMovemetSpeed;

        if(boostTimer < maxBoostTime)
        {
            boostTimer += Time.deltaTime;
        }
        else if(boostTimer >= maxBoostTime)
        {
            boostAvailable = true;
            boostInCooldown = false;
        }
    }
}

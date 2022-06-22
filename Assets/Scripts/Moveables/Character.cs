using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class Character : Moveable
{
    private Action<Vector2> MoveAction;

    [SerializeField]
    [Min(0.1f)]
    private float characterSpeed;

    private PlayerInput playerInput;
    private InputAction moveInputAction;

    void Awake()
    {
        if (MoveAction == null) MoveAction = TurnBasedRoam;
        playerInput = GetComponent<PlayerInput>();
        moveInputAction = playerInput.actions["Move"];

        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        connectedIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            Vector2 input = moveInputAction.ReadValue<Vector2>();
            MoveAction(input);
        }
    }

    /// <summary>
    /// Handle how keyboard presses are handled
    /// </summary>
   /* public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentConnector.Move(this);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            canMove = false;
            nextSection = currentConnector.FindNextClockwiseSection(nextSection);
            //UpdateToNextPositionClockwise();
            currentConnector.Rotate(this, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            canMove = false;
            //UpdateToNextPositionCounterClockwise();
            nextSection = currentConnector.FindNextCounterClockwiseSection(nextSection);
            currentConnector.Rotate(this, -1);
        }
    }*/


    /// <summary>
    /// Updates the move action to free roam
    /// </summary>
    public void FreeRoamActionChange()
    {
        canMove = true;
        MoveAction = FreeRoam;
        playerInput.SwitchCurrentActionMap("Free Roam");
        moveInputAction = playerInput.actions["Move"];
    }

    // /// <summary>
    // /// Handle the movement of the player
    // /// </summary>
    // public void OnMove(InputValue input)
    // {
    //     if (!canMove) return;

    //     Vector2 inputVec = input.Get<Vector2>();

    //     MoveAction(inputVec);
    // }

    private void TurnBasedRoam(Vector2 input)
    {
        if(input.y > 0.0f)
        {
            currentConnector.Move(this);
        }
        else if(input.x > 0.0f)
        {
            canMove = false;
            nextSection = currentConnector.FindNextClockwiseSection(nextSection);
            currentConnector.Rotate(this, 1);
        }
        else if (input.x < 0.0f)
        {
            canMove = false;
            nextSection = currentConnector.FindNextCounterClockwiseSection(nextSection);
            currentConnector.Rotate(this, -1);
        }
    }

    private void FreeRoam(Vector2 input)
    {
        CharacterController cc = GetComponent<CharacterController>();
        Vector3 moveVec = new Vector3(input.x, 0, input.y);
        Vector3 resultVec = Time.deltaTime * characterSpeed * moveVec;

        cc.Move(Time.deltaTime * characterSpeed * moveVec);
    }
}

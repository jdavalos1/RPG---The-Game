using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Moveable
{
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        connectedIndex = 0;
        //nextSection = currentSection.connectedSections[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleInput();
        }
    }

    /// <summary>
    /// Handle how keyboard presses are handled
    /// </summary>
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentConnector.Move(this);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            canMove = false;
            UpdateToNextPositionClockwise();
            currentConnector.Rotate(this, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            canMove = false;
            UpdateToNextPositionCounterClockwise();
            currentConnector.Rotate(this, -1);

        }
    }
}

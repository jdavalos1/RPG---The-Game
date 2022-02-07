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
        if (Input.GetKey(KeyCode.W))
        {
            Move();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            canMove = false;
            connectedIndex++;
            if (connectedIndex >= currentConnector.connectedSections.Count) connectedIndex = 0;

            currentConnector.Rotate(this, 1);
            nextSection = currentConnector.connectedSections[connectedIndex];
        }
        else if (Input.GetKey(KeyCode.A))
        {
            canMove = false;
            connectedIndex--;
            if (connectedIndex < 0) connectedIndex = currentConnector.connectedSections.Count - 1;

            currentConnector.Rotate(this, -1);
            nextSection = currentConnector.connectedSections[connectedIndex];

        }
    }

}

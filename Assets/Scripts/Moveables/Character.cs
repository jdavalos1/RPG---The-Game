using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Moveable
{
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        connectedIndex = 1;
        nextSection = currentSection;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleInput();
        }
    }

    public void HandleInput()
    {
        if (Input.GetKey(KeyCode.W)) Move();
        else if (Input.GetKey(KeyCode.A))
        {
            canMove = false;

            connectedIndex--;
            if(connectedIndex < 1) connectedIndex = currentSection.connectedSections.Count - 1;

            nextSection = currentSection.connectedSections[connectedIndex];
            // Ensure that we cannot traverse to an empty section or if it's a dead end
            // past the map
            if (currentSection.connectedSections.Count < 2) nextSection = null;

            // If there are 2 sections then we know we have to rotate -180, similarly
            // 4 sections (not including the current section) then -90 degrees (i.e. -360 / 4)
            StartCoroutine(Rotate(new Vector3(0, -360 / (currentSection.connectedSections.Count - 1), 0)));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            canMove = false;

            connectedIndex++;
            if (connectedIndex >= currentSection.connectedSections.Count) connectedIndex = 1;

            // Ensure that we cannot traverse to an empty section or if it's a dead end
            // past the map
            if (currentSection.connectedSections.Count < 2) nextSection = null;

            nextSection = currentSection.connectedSections[connectedIndex];

            // If there are 2 sections then we know we have to rotate 180, similarly
            // 4 sections (not including the current section) then 90 degrees (i.e. 360 / 4)
            StartCoroutine(Rotate(new Vector3(0, 360 / (currentSection.connectedSections.Count - 1), 0)));
        }
    }
}

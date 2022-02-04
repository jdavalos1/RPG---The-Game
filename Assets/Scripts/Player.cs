using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool canMove;
    public bool isRotating;

    // Represent the f, l, r, and b rotations
    [Range(0, 3)]
    public int rotationLocation = 0;

    private CharacterController cc;

    [SerializeField]
    private float rotateDuration = 0.5f;
    [SerializeField]
    private float rotateSpeed = 2f;

    [SerializeField]
    private Section currentSection;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        isRotating = false;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleInput();
        }
    }

    public void UpdateCurrentSection(Section newSection)
    {
        if (newSection == null) return;

        currentSection = newSection;
    }


    /// <summary>
    /// Handle what key is pressed by the player
    /// </summary>
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            canMove = false;
            if(rotationLocation == -1) StartCoroutine(DeadEnd(new Vector3(0, -180, 0)));
            else StartCoroutine(currentSection.Move(transform));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            canMove = false;
            isRotating = true;

            if(currentSection.connectedSections.Count <= 2)
            {
                StartCoroutine(Rotate(new Vector3(0, 180, 0)));
            }
            else
            {
                StartCoroutine(Rotate(new Vector3(0, 90, 0)));
            }

            rotationLocation = rotationLocation + 1 < currentSection.connectedSections.Count ?
                               rotationLocation + 1 : currentSection.connectedSections.Count - 1;
            currentSection = currentSection.connectedSections[rotationLocation];
            rotationLocation = currentSection.connectedSections.Count / 2;
            if (currentSection.connectedSections.Count < 2) rotationLocation = -1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            canMove = false;
            isRotating = true;

            if(currentSection.connectedSections.Count <= 2)
            {
                StartCoroutine(Rotate(new Vector3(0, -180, 0)));
            }
            else
            {
                StartCoroutine(Rotate(new Vector3(0, -90, 0)));
            }

            rotationLocation = rotationLocation - 1 > 0 ? rotationLocation - 1 : 0;
            currentSection = currentSection.connectedSections[rotationLocation];
            rotationLocation = currentSection.connectedSections.Count / 2;
            if (currentSection.connectedSections.Count < 2) rotationLocation = -1;
        }

/*        else if (Input.GetKeyDown(KeyCode.D))
        {
            canMove = false;
            isRotating = true;

            float rotation = 90;
            rotationLocation++;
            rotationLocation = rotationLocation > currentSection.connectedSections.Count - 1 ?
                               0 : rotationLocation;

            if (currentSection.connectedSections.Count < 3) rotation = 180;

            Vector3 rotVec = new Vector3(0, rotation, 0);

            if (currentSection.connectedSections.Count == 1) {
                StartCoroutine(DeadEnd(rotVec));
                return;
            }

            currentSection = currentSection.connectedSections[rotationLocation];
            StartCoroutine(Rotate(rotVec));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            canMove = false;
            isRotating = true;

            float rotation = -90;
            rotationLocation--;
            rotationLocation = rotationLocation < 0 ?
                               currentSection.connectedSections.Count - 1 : rotationLocation;

            if (currentSection.connectedSections.Count < 3) rotation = -180;

            Vector3 rotVec = new Vector3(0, rotation, 0);

            if (currentSection.connectedSections.Count == 1) {
                StartCoroutine(DeadEnd(rotVec));
                return;
            }

            currentSection = currentSection.connectedSections[rotationLocation];
            StartCoroutine(Rotate(new Vector3(0, rotation, 0)));
        }*/
    }

    /// <summary>
    /// Rotate to the position passed in over time
    /// </summary>
    /// <param name="newRot">New rotation to add to current for player</param>
    /// <returns></returns>
    public IEnumerator Rotate(Vector3 newRot)
    {
        Vector3 startRotation = transform.eulerAngles;
        Vector3 endRotation = startRotation + newRot;
        float t = 0;

        while (t < rotateDuration)
        {
            t += Time.deltaTime * rotateSpeed;
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, rotateSpeed * t / rotateDuration);
            yield return null;
        }

        isRotating = false;
        canMove = true;
    }

    public IEnumerator DeadEnd(Vector3 rot)
    {
        yield return new WaitForSeconds(1);
        isRotating = true;
        yield return StartCoroutine(Rotate(rot * -1));

        canMove = true;
        isRotating = false;
        rotationLocation = 0;
    }
}

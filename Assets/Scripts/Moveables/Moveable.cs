using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    /// <summary>
    /// Forward move speed of the object
    /// </summary>
    public float moveSpeed = 5;

    /// <summary>
    /// Used to control whether the character can move
    /// </summary>
    public bool canMove;

    /// <summary>
    /// Duration of the character's rotation
    /// </summary>
    [SerializeField]
    private float rotateDuration = 0.5f;
    /// <summary>
    /// Speed at which the character should rotate
    /// </summary>
    [SerializeField]
    private float rotateSpeed = 2f;

    /// <summary>
    /// Current connector the character is on
    /// </summary>
    [SerializeField]
    protected Connector currentConnector;

    /// <summary>
    /// The upcoming section the character will traverse based on the 
    /// rotation of the character
    /// </summary>
    [SerializeField]
    protected Section nextSection;

    /// <summary>
    /// The current index the character is looking towards. Represents
    /// the next section in the list of connected sections
    /// </summary>
    [SerializeField]
    protected int connectedIndex;

    /// <summary>
    /// Move the character based on the next section
    /// </summary>
    public void Move()
    {
        canMove = false;
        StartCoroutine(nextSection.Move(transform));
        // Every section has only 2 connectors no more no less
        int ci = nextSection.connecters.FindIndex(c => c == currentConnector);
        ci++;
        if (ci >= nextSection.connecters.Count) ci = 0;
        currentConnector = nextSection.connecters[ci];
        nextSection = currentConnector.FindNextSection(nextSection);
        Debug.Log(nextSection.name);
    }


    /// <summary>
    /// Rotate the character based on the rotation passed
    /// </summary>
    /// <param name="newRot">Angles of rotation</param>
    /// <returns></returns>
    public IEnumerator Rotate(Vector3 newRot)
    {
        Vector3 startRotation = transform.eulerAngles;
        Vector3 endRotation = startRotation + newRot;
        float t = 0;

        while (t < rotateDuration)
        {
            t += Time.deltaTime * rotateSpeed;
            transform.eulerAngles = Vector3.Lerp(startRotation, endRotation,
                                                 rotateSpeed * t / rotateDuration);
            yield return null;
        }

        canMove = true;
    }

    public bool IsSamePosition(int pos)
    {
        return pos == connectedIndex;
    }

    // TODO - update how the rotation position is found by creating functions
    // that are in the connector to not expose the connected sections to outside
    // use

    public void UpdateToNextPositionCounterClockwise()
    {
        int ci = currentConnector.connectedSections.FindIndex(s => nextSection == s);
        ci--;
        if (ci < 0) ci = currentConnector.connectedSections.Count - 1;
        //nextSection = currentConnector.connectedSections[ci];

        connectedIndex--;
        if (connectedIndex < 0) connectedIndex = currentConnector.connectedSections.Count - 1;

        nextSection = currentConnector.connectedSections[connectedIndex];
    }


    public void UpdateToNextPositionClockwise()
    {
        int ci = currentConnector.connectedSections.FindIndex(s => nextSection == s);
        ci++;
        if (ci >= currentConnector.connectedSections.Count) ci = 0;
        //nextSection = currentConnector.connectedSections[ci];

        connectedIndex++;
        if (connectedIndex >= currentConnector.connectedSections.Count) connectedIndex = 0;
        nextSection = currentConnector.connectedSections[connectedIndex];
    }
}

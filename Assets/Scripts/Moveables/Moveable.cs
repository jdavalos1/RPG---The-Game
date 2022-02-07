using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moveable : MonoBehaviour
{
    public float moveSpeed = 5;
    public bool canMove;

    [SerializeField]
    private float rotateDuration = 0.5f;
    [SerializeField]
    private float rotateSpeed = 2f;

    [SerializeField]
    protected Connector currentConnector;

    [SerializeField]
    protected Section nextSection;

    [SerializeField]
    protected int connectedIndex;

    /// <summary>
    /// Move the character based on the next section
    /// </summary>
    protected void Move()
    {
        canMove = false;
        StartCoroutine(nextSection.Move(transform));
        currentConnector = nextSection.connecters[connectedIndex];
        nextSection = currentConnector.connectedSections[connectedIndex];
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

    /// <summary>
    /// The character has hit a dead end and must go back
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DeadEnd()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Rotate(new Vector3(0, 180, 0)));
        canMove = true;
        nextSection = nextSection.connectedSections[0];
    }
}

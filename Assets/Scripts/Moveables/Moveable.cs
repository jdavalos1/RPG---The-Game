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
    protected Section currentSection;
    [SerializeField]
    protected Section nextSection;

    protected int connectedIndex;

    protected void Move()
    {
        canMove = false;

        if (nextSection == null) StartCoroutine(currentSection.Move(transform));
        else if (currentSection == null && nextSection == null) StartCoroutine(DeadEnd());
        else StartCoroutine(nextSection.Move(transform));

        // Move through the current section and set up the next section
        // based on whether the character has rotated
        if(currentSection == nextSection)
        {
            currentSection = currentSection.connectedSections[0];
            nextSection = currentSection;
        }
        else
        {
            currentSection = nextSection;
            nextSection = nextSection.connectedSections[1];
        }

    }

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

    private IEnumerator DeadEnd()
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Rotate(new Vector3(0, 180, 0)));
        canMove = true;
    }
}

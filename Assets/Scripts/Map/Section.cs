using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    private static readonly float DistanceThreshhold = 0.1f;

    public List<Connector> connecters;

    /// <summary>
    /// Moves the character based on the type of section (i.e. forward section
    /// moves forward)
    /// </summary>
    /// <param name="character">Transform to move</param>
    /// <returns></returns>
    public virtual IEnumerator Move(Transform character) { yield return null; }


    // TOOLS FOR MOVEMENT

    /// <summary>
    /// Tool function to move a transform from one section to another.
    /// This will move the transform in a linear fashion and can only be used by
    /// self or children. Consider start pos to be (0,0,0) and end pos to be
    /// the displacement in each axis.
    /// </summary>
    /// <param name="character">Transform to move forward</param>
    /// <param name="endPos">End location of movement</param>
    /// <returns></returns>
    protected IEnumerator ForwardMovementInSection(Transform character, Vector3 endPos)
    {
        CharacterController cc = character.GetComponent<CharacterController>();
        Vector3 startPos = new Vector3(0, 0, 0), incVec;
        Moveable c = character.GetComponent<Moveable>();

        float dist = Vector3.Distance(startPos, endPos);

        // The distance threshhold will determine how far we move.
        while (dist > DistanceThreshhold)
        {
            float inc = Time.deltaTime * c.moveSpeed;
            incVec = character.forward * inc;
            cc.Move(incVec);
            startPos += incVec;
            dist = Vector3.Distance(endPos, startPos);
            yield return null;
        }
    }
}

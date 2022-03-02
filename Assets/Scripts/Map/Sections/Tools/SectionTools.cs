using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SectionTools
{
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
    public static IEnumerator ForwardMovementInSection(Transform character, Vector3 endPos, float distanceThreshhold)
    {
        CharacterController cc = character.GetComponent<CharacterController>();
        Vector3 startPos = new Vector3(0, 0, 0), incVec;
        Moveable c = character.GetComponent<Moveable>();

        float dist = Vector3.Distance(startPos, endPos);

        // The distance threshhold will determine how far we move.
        while (dist > distanceThreshhold)
        {
            float inc = Time.deltaTime * c.moveSpeed;
            incVec = character.forward * inc;
            cc.Move(incVec);
            startPos += incVec;
            dist = Vector3.Distance(endPos, startPos);
            yield return null;
        }
    }
    
    public static List<Vector3> TranslateVectors(List<Vector3> lossyScales, Quaternion rot)
    {
        for (int i = 0; i < lossyScales.Count; i++)
        {
            Vector3 temp = rot * lossyScales[i];
            lossyScales[i] = new Vector3(Mathf.Abs(temp.x), Mathf.Abs(temp.y), Mathf.Abs(temp.z));
        }

        return lossyScales;
    }

    public static Vector3 TranslateVector(Vector3 lossyScale, Quaternion rot)
    {
        Vector3 temp = rot * lossyScale;
        return new Vector3(Mathf.Abs(temp.x), Mathf.Abs(temp.y), Mathf.Abs(temp.z));
    }
}

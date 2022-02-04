using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSection : Section
{
    [SerializeField]
    private List<GameObject> sections;
    [SerializeField]
    private List<Vector3> secRotations;

    /// <summary>
    /// Handle the forward L movement of the character including rotation.
    /// </summary>
    /// <param name="character">Character to move in the scene</param>
    /// <returns></returns>
    public override IEnumerator Move(Transform character)
    {
        Vector3 localEnd, endPos;
        Moveable c = character.GetComponent<Moveable>();
        int i = 0;
       
        // Iterate through all of the sections for movement. Will move until the 0.5 the size
        // of the next transform in the current forward direction. Rotate after done moving
        for(; i < sections.Count - 1; i++)
        {
            localEnd = sections[i].transform.lossyScale - sections[i+1].transform.lossyScale * 0.5f;
            
            endPos = new Vector3(localEnd.x * character.forward.x,
                                 localEnd.y * character.forward.y,
                                 localEnd.z * character.forward.z);
            yield return StartCoroutine(ForwardMovementInSection(character, endPos));
            yield return StartCoroutine(c.Rotate(secRotations[i]));
            c.canMove = false;
        }

        // Move on the last one 1/2 of the scale of the previous one since we stop
        // at the previous section and not at the current
        localEnd = sections[i - 1].transform.lossyScale * 0.5f + sections[i].transform.lossyScale;
        endPos = new Vector3(localEnd.x * character.forward.x,
                             localEnd.y * character.forward.y,
                             localEnd.z * character.forward.z);
        yield return StartCoroutine(ForwardMovementInSection(character, endPos));

        c.canMove = true;
    }
}

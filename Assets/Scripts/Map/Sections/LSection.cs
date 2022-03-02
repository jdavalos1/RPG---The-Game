using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        float dist1 = Vector3.Distance(character.position, sections[0].transform.position);
        float dist2 = Vector3.Distance(character.position, sections[sections.Count - 1].transform.position);

        if (dist1 < dist2) yield return ForwardMovement(character);
        else yield return BackwardMovement(character);
    }

    private IEnumerator ForwardMovement(Transform character)
    {
        Vector3 localEnd, endPos;
        int i = 0;
        Moveable c = character.GetComponent<Moveable>();

        // Iterate through all of the sections for movement. Will move until the 0.5 the size
        // of the next transform in the current forward direction. Rotate after done moving
        List<Vector3> transScales = sections.Select(s => s.transform.lossyScale).ToList();

        transScales = SectionTools.TranslateVectors(transScales, Quaternion.Euler(transform.localEulerAngles));

        for(; i < transScales.Count - 1; i++)
        {
            localEnd = transScales[i] - 0.5f * transScales[i + 1];
            endPos = new Vector3(localEnd.x * character.forward.x,
                                 localEnd.y * character.forward.y,
                                 localEnd.z * character.forward.z);
            yield return StartCoroutine(SectionTools.ForwardMovementInSection(character, endPos, DistanceThreshhold));
            yield return StartCoroutine(c.Rotate(secRotations[i]));
            c.canMove = false;
        }

        // Move on the last one 1/2 of the scale of the previous one since we stop
        // at the previous section and not at the current
        localEnd = transScales[i - 1] * 0.5f + transScales[i];
        endPos = new Vector3(localEnd.x * character.forward.x,
                             localEnd.y * character.forward.y,
                             localEnd.z * character.forward.z);
        yield return StartCoroutine(ForwardMovementInSection(character, endPos));

        c.canMove = true;
    }

    private IEnumerator BackwardMovement(Transform character)
    {
        Vector3 localEnd, endPos;
        int i = sections.Count - 1;
        Moveable c = character.GetComponent<Moveable>();

        // Iterate through all of the sections for movement. Will move until the 0.5 the size
        // of the next transform in the current forward direction. Rotate after done moving
        List<Vector3> transScales = sections.Select(s => s.transform.lossyScale).ToList();

        transScales = SectionTools.TranslateVectors(transScales, Quaternion.Euler(transform.localEulerAngles));

        for(; i > 0; i--)
        {
            localEnd = transScales[i - 1] * 0.5f + transScales[i];
            endPos = new Vector3(localEnd.x * character.forward.x,
                                 localEnd.y * character.forward.y,
                                 localEnd.z * character.forward.z);
            yield return StartCoroutine(ForwardMovementInSection(character, endPos));
            yield return StartCoroutine(c.Rotate(-secRotations[i - 1]));
            c.canMove = false;
        }

        // Move on the last one 1/2 of the scale of the previous one since we stop
        // at the previous section and not at the current
        localEnd = transScales[i] - 0.5f * transScales[i + 1];
        endPos = new Vector3(localEnd.x * character.forward.x,
                             localEnd.y * character.forward.y,
                             localEnd.z * character.forward.z);
        yield return StartCoroutine(ForwardMovementInSection(character, endPos));

        c.canMove = true;
    }
}

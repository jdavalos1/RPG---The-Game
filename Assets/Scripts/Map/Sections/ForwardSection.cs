using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class ForwardSection : Section
{
    public override IEnumerator Move(Transform character)
    {
        Moveable p = character.GetComponent<Moveable>();

        Vector3 transScale = SectionTools.TranslateVector(transform.lossyScale, Quaternion.Euler(transform.eulerAngles));

        Vector3 endPos = new Vector3(transScale.x * character.forward.x,
                                     transform.lossyScale.y * character.forward.y,
                                     transform.lossyScale.z * character.forward.z);

        yield return StartCoroutine(SectionTools.ForwardMovementInSection(character, endPos, DistanceThreshhold));

        p.canMove = true;
    }
}
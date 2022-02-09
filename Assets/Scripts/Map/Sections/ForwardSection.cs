using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class ForwardSection : Section
{
    public override IEnumerator Move(Transform character)
    {
        Moveable p = character.GetComponent<Moveable>();
        Vector3 endPos = new Vector3(transform.lossyScale.x * character.forward.x,
                                     transform.lossyScale.y * character.forward.y,
                                     transform.lossyScale.z * character.forward.z);

        yield return StartCoroutine(ForwardMovementInSection(character, endPos));

        p.canMove = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class ForwardSection : Section
{
    /// <summary>
    /// Handle how the player moves in the forward direction.
    /// </summary>
    /// <param name="character">Character to move in the scene</param>
    /// <returns></returns>
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
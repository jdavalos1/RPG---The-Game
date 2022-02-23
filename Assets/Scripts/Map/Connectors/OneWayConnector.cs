using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayConnector : Connector
{
    /// <summary>
    /// Rotation needed to face the next section
    /// </summary>
    [SerializeField]
    private Vector3 rotation;

    /// <summary>
    /// Index location of the dead end object
    /// </summary>
    [SerializeField]
    private int deadEndPosition;

    public override void Move(Moveable character)
    {
        if (character.IsSamePosition(deadEndPosition)) StartCoroutine(DeadEnd(character));
        else character.Move();
    }

    public override void Rotate(Moveable character, int dir)
    {
        StartCoroutine(character.Rotate(dir * rotation));
    }

    /// <summary>
    /// Ensures the character never passes the dead end section by rotating the character
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    private IEnumerator DeadEnd(Moveable character)
    {
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(character.Rotate(rotation));
        character.UpdateNextSection();
        //character.UpdateToNextPositionClockwise();
        character.canMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayConnector : Connector
{
    /// <summary>
    /// Rotation of the location
    /// </summary>
    [SerializeField]
    Vector3 rotation;

    public override void Rotate(Moveable character, int dir)
    {
        StartCoroutine(character.Rotate(dir * rotation));
    }

    public override void Move(Moveable character)
    {
        character.Move();
    }
}

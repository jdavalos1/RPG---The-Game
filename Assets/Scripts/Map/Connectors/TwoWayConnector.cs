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

    /// <summary>
    /// Rotate the character based on the rotation of the
    /// connector and the direction
    /// </summary>
    /// <param name="character"></param>
    /// <param name="dir"></param>
    public override void Rotate(Moveable character, int dir)
    {
        StartCoroutine(character.Rotate(dir * rotation));
    }
}

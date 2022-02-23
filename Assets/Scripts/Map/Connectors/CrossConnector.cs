using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossConnector : Connector
{
    [SerializeField]
    Vector3 rotation;

    public override void Move(Moveable character)
    {
        character.Move();
    }

    public override void Rotate(Moveable character, int dir)
    {
        StartCoroutine(character.Rotate(dir * rotation));
    }
}

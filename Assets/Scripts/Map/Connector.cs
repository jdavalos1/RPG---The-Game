using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Connector : MonoBehaviour
{
    [SerializeField]
    public List<Section> connectedSections;

    /// <summary>
    /// Rotate the character passed in based on the direction of the movement
    /// (i.e. -1 or 1)
    /// </summary>
    /// <param name="character">Character that moves</param>
    /// <param name="dir">Direction of movement (-1 or 1)</param>
    public abstract void Rotate(Moveable character, int dir);

    /// <summary>
    /// Move the character passed in based on the section movement and the
    /// type of connector.
    /// </summary>
    /// <param name="character">Character to move</param>
    public abstract void Move(Moveable character);
}

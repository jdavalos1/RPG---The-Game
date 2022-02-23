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

    /// <summary>
    /// Obtains the section in front of the current section
    /// based on the connector and its length
    /// </summary>
    /// <param name="s">current section traversing thorugh</param>
    /// <returns>Next section being faced</returns>
    public Section FindNextSection(Section s)
    {
        // locate next section find the previous section's location
        // in the current connector's list of connected sections
        // then add 1/2 of the current section's length
        int secIndex = connectedSections.FindIndex(i => i == s);
        secIndex += connectedSections.Count / 2;
        secIndex %= connectedSections.Count;
        return connectedSections[secIndex];
    }

    public Section LocateNextSection(Transform character)
    {
        // prev solution
        Section s = connectedSections.Find(c =>
        {
            Vector3 AtoB = (c.transform.position - character.position).normalized;
            float dotProd = Vector3.Dot(AtoB, character.forward);
            return dotProd < 0.1f;
        });

        return s;
    }
}

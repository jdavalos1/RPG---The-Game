using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Connector : MonoBehaviour
{
    [SerializeField]
    public List<Section> connectedSections;

    public abstract void Rotate(Moveable character, int dir);
}

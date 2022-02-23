using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance;

    [SerializeField]
    private Section beginnningSection;

    void Awake()
    {
        instance = this;
    }
}

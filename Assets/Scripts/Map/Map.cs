using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map instance;
    Player player;

    void Awake()
    {
        instance = this;
    }
}

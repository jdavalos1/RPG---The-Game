using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSection : Section 
{
    public override IEnumerator Move(Transform character)
    {
        character.rotation = new Quaternion();
        SceneManager.LoadScene("RoomScene");
        character.GetComponent<Character>().FreeRoamActionChange();
        yield return null;
    }
}

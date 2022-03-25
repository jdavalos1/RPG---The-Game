using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSection : Section 
{
    public override IEnumerator Move(Transform character)
    {
        DontDestroyOnLoad(character);
        character.GetComponent<CharacterOpenRoom>().enabled = true;
        character.GetComponent<Character>().enabled = false;
        character.rotation = new Quaternion();
        SceneManager.LoadScene("RoomScene");
        yield return null;
    }
}

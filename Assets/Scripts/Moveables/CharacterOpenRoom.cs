using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOpenRoom : MonoBehaviour
{
    CharacterController cc;
    [SerializeField]
    private float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        cc.Move(Time.deltaTime * playerSpeed * move);
    }
}

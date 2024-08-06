using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private List<string> listCharacter; //Luu lai ten cac doi tuong da qua canh cong
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.Player_Tag))
        {
            Character character=other.GetComponent<Character>();
       
        }
    }
}

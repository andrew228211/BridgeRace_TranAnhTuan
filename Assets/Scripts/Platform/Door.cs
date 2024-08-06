using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private Platform _platform;
    [SerializeField] private MeshRenderer _mesh;
    public Platform GetPlatform()
    {
        return this._platform;
    }
    public void TurnOffMesh()
    {
        _mesh.enabled = false;
    }

    public void TurnOnTrigger()
    {
        _collider.isTrigger = true;
    }
    public void TurnOffTrigger()
    {
        _collider.isTrigger = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.Player_Tag) || other.CompareTag(TagManager.Bot_Tag))
        {
            Character character = other.GetComponent<Character>();
            TurnOffMesh();           
            if (character.IsOnGround)
            {
                character.oldPlatform.TriggerDoor();
            }
            else if (_platform != character.oldPlatform)
            {
                character.ChangePlatform(_platform);
            }
        }
    }
}

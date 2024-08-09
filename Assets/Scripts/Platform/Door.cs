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
        if (other.CompareTag(TagManager.Player_Tag))
        {
            Character character = other.GetComponent<Character>();
            TurnOffMesh();
            if (character.IsOnGround)
            {

                Player player = character as Player;
                player.SetMoveSpeed(0);
            }
            if (_platform != character.oldPlatform)
            {
                character.ChangePlatform(_platform);
            }
        }
        else if (other.CompareTag(TagManager.Bot_Tag)){
            Bot character = other.GetComponent<Bot>();
            if (character.NumberBrickToPassPlatform <= 0)
            {
                TurnOffMesh();
                character.RemoveAllTargetInList();
                if (_platform != character.oldPlatform)
                {
                    character.ChangePlatform(_platform);
                }
                character.UpdateNumberBrickToCollect();
                Debug.Log(character.NumberBrickToPassPlatform + " not");
                character.ChangeState(character._botMoveState);
            }          
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.TryGetComponent<Player>(out Player player))
    //    {
    //        player.SetMoveSpeed(5);
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
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
}

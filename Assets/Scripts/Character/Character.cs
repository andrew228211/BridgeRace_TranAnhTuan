using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private LayerMask _stepLayer;
    [SerializeField] protected TextMeshProUGUI txtName;
    [SerializeField] private ColorData _colorData;
    [SerializeField] private TypeColor _color;
    [Header("Movement")]
    public Transform tfrm;
    public  Rigidbody rb;
    public  Animator anim;
    public Platform oldPlatform;
    [Header("Change Brick")]
    [SerializeField] private Transform _parentBrick;
    [SerializeField] private Stack<Brick> _stackBrick;
    [SerializeField] private Transform _image;
    [SerializeField] private Vector3 _offset;
    [Header("Ladder")]
    public bool checkLadder;
    private bool isColider;
    public virtual void Init() {
        _stackBrick = new Stack<Brick>();
        isColider = false;
    }
    public virtual void StopMoving()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(TagManager.Brick_Tag))
        {
            Brick brick = other.gameObject.GetComponent<Brick>();
            if (brick.color == _color || brick.color.Equals(TypeColor.none))
            {
                if (!brick.IsCollected)
                {
                    brick.ChangeColor(_color);
                    AddBrick(brick);
                }
            }
        }
        else if (other.collider.CompareTag(TagManager.Step_Tag))
        {
            Step step = other.gameObject.GetComponent<Step>();
            if (step.GetColor() != _color)
            {
                if (_stackBrick.Count > 0)
                {
                    checkLadder = true;
                    step.SetColor(_colorData.GetMat(_color), _color);
                    BuildBridge();
                }
            }
            if (step.GetColor() != _color && _stackBrick.Count == 0)
            {
                checkLadder = false;
            }
            if (step.GetColor() == _color)
            {
                checkLadder = true;
            }
        }
        //else if (other.collider.CompareTag(TagManager.Ground_Tag))
        //{
        //    oldPlatform.TriggerDoor();
        //    Debug.Log("Ground Collision");
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.Door_Tag))
        {         
            Door door = other.gameObject.GetComponent<Door>();
            door.TurnOffMesh();
            Platform platform = door.GetPlatform();
            if (oldPlatform == platform)
            {
                oldPlatform.TriggerDoor();
            }
            else if (platform != oldPlatform)
            {
                RemoveAllBrick();
                ChangePlatform(platform);
            }
        }
    }
    private void AddBrick(Brick brick)
    {
        oldPlatform.SetPosForListPosBrick(brick.tfrmBrick.position);
        brick.tfrmBrick.parent = _image;
        brick.Collect();
        if(_stackBrick.Count> 0)
        {
            brick.tfrmBrick.localPosition = _offset + _stackBrick.Peek().transform.localPosition;           
        }
        else
        {
            brick.tfrmBrick.localPosition = _offset;
        }
        brick.transform.localRotation = Quaternion.Euler(Vector3.zero);
        _stackBrick.Push(brick);      
    }

    private void RemoveAllBrick()
    {
        while (_stackBrick.Count > 0)
        {
            Brick brick = _stackBrick.Pop();
            brick.gameObject.SetActive(false);
        }
    }
    private void CollisionCharacter()
    {
        StartCoroutine(IDelayReMoveBrick());
    }
    IEnumerator IDelayReMoveBrick()
    {
        while (_stackBrick.Count > 0)
        {
            Brick brick = _stackBrick.Pop();
            brick.Explode(brick.transform .position, _parentBrick);
            Debug.Log(brick.gameObject.name);
            yield return null;
        }
    }
    private void BuildBridge()
    {
        if (_stackBrick != null)
        {
            Brick brick = _stackBrick.Peek();
            brick.transform.parent = _parentBrick;
            brick.ResetBrick();
            brick.gameObject.SetActive(false);
            _stackBrick.Pop();
            oldPlatform.GenerateBrick(1, _color);
        }
    }

    public void ChangePlatform(Platform platform)
    {
        if (oldPlatform != null)
        {
            oldPlatform.HideBrickAfterChararacterPass(_color);
            oldPlatform = platform;
            oldPlatform.UpdateBrick(_color);
        }   
    }

    #region Raycast to check move ladder
    protected bool CheckMove(Vector3 newPos)
    {
        bool move = true;
        Debug.DrawRay(newPos, Vector3.down * 4f, Color.red);
        if (Physics.Raycast(newPos, Vector3.down, out RaycastHit hit, 4f, _stepLayer))
        {
            Step step = hit.collider.GetComponent<Step>();
            if (step.GetColor() != _color)
            {
                if (_stackBrick.Count > 0)
                {
                    move = true;
                }
            }
            if(step.GetColor() != _color && _stackBrick.Count == 0)
            {
                move = false;
            }
        }
        return move;
    }
    private Vector3 GetDirectioin()
    {
        return tfrm.TransformDirection(Vector3.forward);
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{

    [Header("Movement")]
    [SerializeField] private DynamicJoystick _joystick;
    [Header("Move")]
    [SerializeField] private float _moveSpeed;
    private Vector3 _moveDirection;
    [SerializeField] private Transform _pointCheckRaycast;
    private Vector3 newPoint;
    public override void Init()
    {
        base.Init();

    }
    public override void StopMoving()
    {
        base.StopMoving();
        rb.velocity = Vector3.zero;
        anim.SetFloat("velocity", 0);
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        // Debug.Log(checkLadder);
        
        
        _moveDirection = _joystick.GetDirection3V();
        if (_moveDirection.z > 0)
        {
            newPoint = _pointCheckRaycast.position;
        }
        else
        {
            newPoint = _pointCheckRaycast.position * -1;
        }
        if (!CheckMove(newPoint))
        {
            return;
        }

        Debug.Log(_moveDirection);
        tfrm.position += new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * _moveSpeed * Time.deltaTime;
        if (_joystick.Direction.sqrMagnitude <= 0.1f) StopMoving();
        if (Mathf.Abs(_moveDirection.x) > 0.01f || Mathf.Abs(_moveDirection.y) > 0.01f) 
        {
            Rotate(_moveDirection);
            anim.SetFloat("velocity", 1);
        }
        else
        {
            anim.SetFloat("velocity", 0);
        }
    }
    private void Rotate(Vector3 dir)
    {
        tfrm.rotation = Quaternion.Lerp(tfrm.rotation, Quaternion.LookRotation(dir.normalized, Vector3.up), 5 * Time.deltaTime);
    }
}

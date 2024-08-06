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
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.CompareTag(TagManager.Wall_Tag))
        {
            _moveSpeed = 0;
        }
        else if (collision.collider.CompareTag(TagManager.Door_Tag))
        {
            _moveSpeed = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(TagManager.Wall_Tag))
        {
            _moveSpeed = 5;
        }
        else if (collision.collider.CompareTag(TagManager.Door_Tag))
        {
            _moveSpeed = 5;
        }
    }
    private void Move()
    {      
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
        tfrm.position += new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) * _moveSpeed * Time.deltaTime;
        if (Mathf.Abs(_moveDirection.x) > 0.001f || Mathf.Abs(_moveDirection.y) > 0.001f) 
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

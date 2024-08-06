using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Transform _posBridge;
    [SerializeField] private float _lenBridge;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _posInit;
    [SerializeField] private Stack<GameObject> _stackStep;
    public void OnInit()
    {
        int num = (int)Mathf.Round(_lenBridge / _offset.y);
        _stackStep = new Stack<GameObject>();
        for(int i= 0; i < num; i++)
        {
            GameObject X = ObjectPool.Instance.GetObjet("step");
            X.transform.parent = _posBridge;
            if (_stackStep.Count>0)
            {
                X.transform.localPosition = _stackStep.Peek().transform.localPosition + _offset;               
            }
            else
            {
                X.transform.localPosition = _posInit;              
            }
            _stackStep.Push(X);
            X.transform.localRotation = Quaternion.Euler(Vector3.zero);
            //Debug.Log(X.transform.localRotation);
        }
    }
}

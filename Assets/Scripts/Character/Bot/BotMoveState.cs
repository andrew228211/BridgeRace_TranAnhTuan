using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMoveState : IState<Bot>
{
    Vector3 targetPos = Vector3.zero;
    private float thresold = 0.1f;
    private bool checkTarget;
    public void OnEnter(Bot t)
    {
        SetupPos(t);
        t.anim.SetFloat("velocity", 0);
        checkTarget = false;
    }

    public void OnExecute(Bot t)
    {
        if (t.agent.velocity.magnitude != 0)
        {
            t.tfrm.rotation = Quaternion.LookRotation(t.agent.velocity);
            t.anim.SetFloat("velocity", 1);
        }
        else
        {
            t.anim.SetFloat("velocity", 0);
        }
        if (t.numberBrickToCollect <= 0)
        {
            t.ChangeState(t._botBuildState);
        }
        if (Vector3.Distance(t.tfrm.position,targetPos)<thresold)
        {
            SetupPos(t);
        }
    }
    public void OnExit(Bot t)
    {

    }
    private void SetupPos(Bot t)
    {
        
        if (t.numberBrickToCollect <= 0)
        {
            Bridge bridge = t.oldPlatform.GetBridge(t);
            targetPos = bridge.GetFirstPos();
            checkTarget = true;
        }
        else
        {
            targetPos = t.GetTargetBrick();
            checkTarget = false;
        }
        t.SetDestination(targetPos);
    }
}

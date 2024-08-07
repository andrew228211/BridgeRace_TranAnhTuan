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
        if (Vector3.Distance(t.tfrm.position,targetPos)<thresold)
        {
            if (checkTarget)
            {
                Debug.Log(t.numberBrickToCollect);
                t.ChangeState(t._botBuildState);
            }
            else
            {
                t.numberBrickToCollect -= 1;
                SetupPos(t);
            }
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
        Debug.Log(t.numberBrickToCollect + " pos: " + targetPos );
        t.SetDestination(targetPos);
    }
}

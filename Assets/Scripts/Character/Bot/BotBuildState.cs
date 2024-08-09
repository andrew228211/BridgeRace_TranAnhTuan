using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBuildState : IState<Bot>
{
    Vector3 targetPos=Vector3.zero;
    public void OnEnter(Bot t)
    {
        Bridge brige = t.oldPlatform.GetBridge(t);
        targetPos = brige.GetLastBot(t);
        t.SetDestination(targetPos);
        t.UpdateNumberBrickToCollect();
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
        if (!t.CheckImageBrick())
        {          
            t.ChangeState(t._botMoveState);

        }
    }

    public void OnExit(Bot t)
    {
        
    }
}


using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public Camera cam;
    public NavMeshAgent agent;

    // ----------State--------------------
    public StateMachine<Bot> _stateMachine;
    public BotBuildState _botBuildState;
    public BotCollectState _botCollectState;  
    public BotMoveState _botMoveState;

    private int numberBrickToPassPlatform;
    public int numberBrickToCollect;
    public int NumberBrickToPassPlatform
    {
        get { return numberBrickToPassPlatform; }
        set { numberBrickToPassPlatform = value; } 
    }
    #region Set up target brick for bot
    private Stack<Vector3> stackTargetBricks;
    public void SetTargetBrick(Vector3 pos)
    {
        stackTargetBricks.Push(pos);
    }
    public bool HaveStackBrick()
    {
        if (stackTargetBricks.Count == 0) return false;
        return true;
    }
    public Vector3 GetTargetBrick()
    {
        Debug.Log(stackTargetBricks.Count);
        return stackTargetBricks.Pop();
    }
    #endregion
    private void Awake()
    {
        stackTargetBricks = new Stack<Vector3>();     
    }
    private void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        agent.SetDestination(hit.point);
        //    }
        //}
        _stateMachine.Update();
    }
    public override void Init()
    {
        base.Init();
        numberBrickToPassPlatform = oldPlatform.numBrickToPass;
        Debug.Log(numberBrickToCollect);
        numberBrickToCollect = Random.Range(1, numberBrickToPassPlatform - 2);
        InitState();
      
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    public override void StopMoving()
    {
        base.StopMoving();
        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }
        agent.enabled = false;
    }
    private void InitState()
    {
        _stateMachine = new StateMachine<Bot>(this);
        _botBuildState = new BotBuildState();
        _botCollectState = new BotCollectState();
        _botMoveState = new BotMoveState();
        _stateMachine.ChangeState(_botMoveState);
    }
    public void ChangeState(IState<Bot> state)
    {
        _stateMachine.ChangeState(state);
    }
    public void SetDestination(Vector3 pos)
    {
        agent.enabled = true;
        agent.SetDestination(pos);
    }
}

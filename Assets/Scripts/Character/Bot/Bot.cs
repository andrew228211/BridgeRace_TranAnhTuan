using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;

    // ----------State--------------------
    public StateMachine<Bot> _stateMachine;
    public BotBuildState _botBuildState;
 
    public BotMoveState _botMoveState;

    private int numberBrickToPassPlatform;
    public int numberBrickToCollect;
    public int numberBrick { get; private set; }
    private Bridge _bridgeToBuild;
    public Bridge BuildBridge 
    {
        get { return _bridgeToBuild; }
        set { _bridgeToBuild = value; }
    }
    public int NumberBrickToPassPlatform
    {
        get { return numberBrickToPassPlatform; }
        set { numberBrickToPassPlatform = value; } 
    }
    #region Set up target brick for bot
    [SerializeField] private List<Vector3> _listTargetBricks;
    public void SetTargetBrick(Vector3 pos)
    {
        _listTargetBricks.Add(pos);
    }
    public bool HaveStackBrick()
    {
        if (_listTargetBricks.Count == 0) return false;
        return true;
    }
    public Vector3 GetTargetBrick()
    {
        return _listTargetBricks[0];
    }
    public void RemoveAllTargetInList()
    {
        _listTargetBricks.Clear();
    }
    public void RemovePosInStackAfterCollision(Vector3 pos)
    {
        _listTargetBricks.Remove(pos);
        numberBrickToCollect -= 1;
        numberBrickToPassPlatform -= 1;
    }
    #endregion
    private void Awake()
    {
        _listTargetBricks = new List<Vector3>();   
    }
    private void Update()
    {
        _stateMachine.Update();
    }
    public void UpdateNumberBrickToCollect()
    {
        numberBrickToCollect = Random.Range(1,Mathf.Max(1,Mathf.Min(numberBrickToPassPlatform,10)));
    }
    public override void Init()
    {
        base.Init();
        numberBrickToPassPlatform = oldPlatform.numBrickToPass;
        UpdateNumberBrickToCollect();
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
        rb.velocity = Vector3.zero;
        agent.velocity = Vector3.zero;
        agent.enabled = false;
    }
    private void InitState()
    {
        _stateMachine = new StateMachine<Bot>(this);
        _botBuildState = new BotBuildState();
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

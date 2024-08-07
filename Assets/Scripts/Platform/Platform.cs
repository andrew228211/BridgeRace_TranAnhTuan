using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Platform : MonoBehaviour
{
    [Header("Bridge")]
    [SerializeField] private Bridge[] bridges;
    [SerializeField] private List<Bridge> listBridgeBotHaveMoves;
    [SerializeField] private Door[] doors;
    [Header("Brick")]
    [SerializeField] private List<Vector3> listPosBrick; //Khoi tao ban dau de chua
    [SerializeField] private List<Brick> listActiveBrick;
    [SerializeField] private Transform parentBrick;
    [SerializeField] private int w;
    [SerializeField] private int h;
    public int numBrickToPass { get; private set; }
    public void Onit()
    {
        InitBrick();
        //InitBridge();
        InitDoor();
        numBrickToPass = 20;
    }
    private void InitDoor()
    {
        if (doors.Length>0)
        {
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].TurnOnTrigger();
            }
        }
    }
    #region Handle Bridge
    public Bridge GetBridge(Character bot)
    {
        listBridgeBotHaveMoves.Clear();
        foreach(Bridge bridge in bridges)
        {
            if (bridge.CheckFirstStep(bot))
            {
                listBridgeBotHaveMoves.Add(bridge);
            }
        }
        if (listBridgeBotHaveMoves.Count > 0)
        {
            return listBridgeBotHaveMoves[Random.Range(0, listBridgeBotHaveMoves.Count)];
        }
        else
        {
            return bridges[Random.Range(0, bridges.Length)];
        }
    }
    #endregion
    private void InitBridge()
    {
        for(int i=0; i < bridges.Length; i++)
        {
            bridges[i].OnInit();
        }
    }
    public void TriggerDoor()
    {
        if (doors.Length > 0)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].TurnOffTrigger();
            }
        }
    }
    #region Handle Brick
    private void InitBrick()
    {
        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {
                Vector3 pos = new Vector3(2*i-w,0.5f,2*j-h) + new Vector3(1.5f,0,2f);
                listPosBrick.Add(pos);
            }
        }
    }
    private Brick SpawnBrick(Vector3 pos)
    {
        Brick brick = ObjectPool.Instance.GetObjet("brick").GetComponent<Brick>();
        brick.transform.position = pos;
        brick.transform.parent = parentBrick;
        return brick;
    }


    public void GenerateBrick(int num, TypeColor color,Character character=null)
    {
        Bot bot = null;
        if (character != null)
        {
            bot = character as Bot;        
        }

        for (int i = 0; i < num && listPosBrick.Count > 0; i++)
        {
            int pos=Random.Range(0,listPosBrick.Count);
            Brick brick = SpawnBrick(listPosBrick[pos]);
            brick.ChangeColor(color);
            listActiveBrick.Add(brick);
            if (bot != null)
            {
                bot.SetTargetBrick(brick.tfrmBrick.position);
            }
            listPosBrick.Remove(listPosBrick[pos]);
        }
    }
    public void SetPosForListPosBrick(Vector3 pos)
    {
        listPosBrick.Add(pos);
    }


    public void HideBrickAfterChararacterPass(TypeColor color)
    {
        for(int i = 0; i < listActiveBrick.Count; i++)
        {
            if(listActiveBrick[i].color == color)
            {
                listActiveBrick[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateBrick(TypeColor color)
    {
        GenerateBrick(6, color);
    }
    #endregion
}

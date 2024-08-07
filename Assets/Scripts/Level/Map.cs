using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] Character player;
    [SerializeField] Character[] bots;
    [SerializeField] Transform[] characterTfs;
    [SerializeField] List<Platform> listPlatforms;
    private void Start()
    {
        OnIt();
    }
    private void OnIt()
    {        
        foreach(Platform platform in listPlatforms)
        {
            platform.Onit();
        }
        InitPlayer();
        InitBots();
    }
    private void InitPlayer()
    {
        player.tfrm.position = characterTfs[0].position;
        player.gameObject.SetActive(true);
        player.oldPlatform = listPlatforms[0];
        player.SetColor(1);
        listPlatforms[0].GenerateBrick(18, player.GetColor());
    }
    private void InitBots()
    {
        for(int i = 0; i < bots.Length; i++)
        {
            bots[i].tfrm.position = characterTfs[i+1].position;
            bots[i].gameObject.SetActive(true);
            bots[i].oldPlatform = listPlatforms[0];
            bots[i].SetColor(i + 2);
            listPlatforms[0].GenerateBrick(18, bots[i].GetColor(), bots[i]);
            bots[i].Init();
        }
    }
}

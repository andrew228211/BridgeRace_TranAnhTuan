using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] Character player;
    [SerializeField] Transform[] characterTfs;
    [SerializeField] private int numPlayer;
    [SerializeField] List<Platform> listPlatforms;
    TypeColor playerColor;
    private void Start()
    {
        OnIt();
    }
    private void OnIt()
    {
        playerColor = TypeColor.green;
        foreach(Platform platform in listPlatforms)
        {
            platform.Onit();
        }
        //for(int i = 0; i < numPlayer; i++)
        //{
        //    if (i != (int)playerColor)
        //    {
              
        //    }
        //    else
        //    {
        //        player.tfrm.position = characterTfs[i].position;
        //        player.gameObject.SetActive(true);
        //        listPlatforms[0].GenerateBrick(36,playerColor);
        //    }
        //}
        player.tfrm.position = characterTfs[0].position;
        player.gameObject.SetActive(true);
        player.oldPlatform = listPlatforms[0];
        listPlatforms[0].GenerateBrick(36, playerColor);
    }
   
}

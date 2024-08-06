using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] ColorData colorData;
    [SerializeField] Character characters;
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
            platform.GenerateBrick(35, TypeColor.green);
        }
        for(int i = 0; i < numPlayer; i++)
        {
            if (i != (int)playerColor)
            {
              
            }
        }
    }
   
}

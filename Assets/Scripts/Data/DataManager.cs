using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
   public int Coin
   {
        get { return PlayerPrefs.GetInt("_coin"); }
        set { PlayerPrefs.SetInt("_coin", value); }
   }
   public int Diamond
   {
        get { return PlayerPrefs.GetInt("_diamond"); }
        set { PlayerPrefs.SetInt("_diamond", value); }
   }
    public int Block
    {
        get { return PlayerPrefs.GetInt("_block"); }
        set { PlayerPrefs.SetInt("_block", value); }
    }

    public int CurrentLevel
    {
        get { return PlayerPrefs.GetInt("_level"); }
        set { PlayerPrefs.SetInt("_level", value); }
    }
}

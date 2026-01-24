using System;
using UnityEngine;

public class UserGoodsData : IUserData
{
    // 유저재화.
    // 보석
    public long Gem { get; set; }
    // 골드.
    public long Gold { get; set; }
    
    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");

        Gem = 0;
        Gold = 0;
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;
        try
        {
            Gem = long.Parse(PlayerPrefs.GetString("Gem"));
            Gold = long.Parse(PlayerPrefs.GetString("Gold"));
            result = true;
            
            Logger.Log($"Gem : {Gem}, Gold : {Gold}");
        }
        catch (Exception e)
        {
            Logger.Log("LoadData failed ("+ e.Message + ")");
        }
        return result;
    }

    public bool Savedata()
    {
        Logger.Log($"{GetType()}::Savedata");
        bool result = false;

        try
        {
            PlayerPrefs.SetString("Gem", Gem.ToString());
            PlayerPrefs.SetString("Gold", Gold.ToString());
            result = true;
            
            Logger.Log($"Gold : {Gold}, Gem : {Gem}");
        }
        catch (Exception e)
        {
            Logger.Log("Savedata failed ("+ e.Message + ")");
        }
        
        return result;
    }
}

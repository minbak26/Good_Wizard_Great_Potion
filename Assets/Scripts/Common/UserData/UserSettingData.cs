using System;
using UnityEngine;

public class UserSettingData : IUserData
{
    public bool Sound { get; set; }
    
    
    
    public void SetDefaultData()
    {
        Logger.Log($"{GetType()}::SetDefaultData");
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;
        
        try
        {
            Sound = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
            result = true;
            
            Logger.Log($"Sound : {Sound}");
        }
        catch (Exception e)
        {
            Logger.Log($"Load failed ( " +  e.Message + ")");
        }
        
        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::Savedata");
        bool result = false;
        
        try
        {
            PlayerPrefs.SetInt("Sound", Sound ? 1 : 0);
            result = true;
            
            Logger.Log($"Sound : {Sound}");
        }
        catch (Exception e)
        {
            Logger.Log($"Load failed ( " +  e.Message + ")");
        }
        return result;
    }
}

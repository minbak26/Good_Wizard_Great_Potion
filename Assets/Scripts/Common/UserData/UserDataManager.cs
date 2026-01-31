using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
   
   // 저장된 유저데이터의 존재 여부.
   public bool ExistsSavedData {get; private set;}
   
   // 모든 유저 데이터 인스턴스를 저장하는 컨테이너
   public List<IUserData> UserDataList {get; private set;} = new List<IUserData>();

   protected override void Init()
   {
      // 부모 맴버먼저 호출.
      base.Init();
      // 모든 유저 데이터를 UserDataList에 추가.
      UserDataList.Add(new UserSettingData());
      UserDataList.Add(new UserGoodsData());
   }

   public void SetDefaultUserData()
   {
      for (int i = 0; i < UserDataList.Count; i++)
      {
         UserDataList[i].SetDefaultData();
      }
   }

   public void LoadUserData()
   {
      ExistsSavedData = PlayerPrefs.GetInt("ExistsSavedData") == 1? true:false;
      if (ExistsSavedData)
      {
         for (int i = 0; i < UserDataList.Count; i++)
         {
            UserDataList[i].LoadData();
         }
      }
   }

   public void SaveUserData()
   {
      bool hasSaveError = false;
      for (int i = 0; i < UserDataList.Count; i++)
      {
         bool isSaveError = UserDataList[i].SaveData();
         if (!isSaveError)
         {
            hasSaveError = true;
         }
         
      }

      if (!hasSaveError)
      {
         ExistsSavedData = true;
         PlayerPrefs.SetInt("ExistsSavedData", 1);
         PlayerPrefs.Save();
      }
   }
}

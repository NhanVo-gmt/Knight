using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    bool IsLoadFirstTime();
    void LoadData(GameData gameData); 
    void SaveData(ref GameData data);
}

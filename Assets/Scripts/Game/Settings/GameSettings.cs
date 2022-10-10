using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;

    public AttackData TrapSettings;

    void Awake() 
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    
}

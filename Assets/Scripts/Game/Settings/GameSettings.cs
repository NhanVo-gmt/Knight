using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;

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

    [Header("Player")]
    public int maxHealth;

    [Header("Trap")]
    public AttackData TrapSettings;
    
}

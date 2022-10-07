using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Data/Player/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public JumpData jumpData;
    public MoveData moveData;
    public PlayerAttackData meleeAttackData;
    
}

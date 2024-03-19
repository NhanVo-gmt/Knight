using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.Editor
{
    public static class BehaviourTreeEditorNodeSettings
    {
        public static string[] AnimationClipNames = {
            "Idle", "Idle1", 
            "Move", 
            "Die",
            "Tele", "Teleport", "Teleback",
            "ReadyToAttack", "Attack", "RecoverFromAttack",
            "ReadyToAttack2", "Attack2", "RecoverFromAttack2",
            "ReadyToAttack3", "Attack3", "RecoverFromAttack3",
            "Fly", "Hit"};
    }
}

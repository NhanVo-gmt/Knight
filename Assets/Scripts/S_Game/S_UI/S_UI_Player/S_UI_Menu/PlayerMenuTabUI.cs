using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knight.UI
{
    public class PlayerMenuTabUI : MonoBehaviour
    {
        public enum Tab
        {
            Character,
            Inventory,
            Skills,
        }

        [SerializeField] private Tab tab;

        public void OpenTab()
        {
            
        }

        public Tab GetTab()
        {
            return tab;
        }
    }
    
}

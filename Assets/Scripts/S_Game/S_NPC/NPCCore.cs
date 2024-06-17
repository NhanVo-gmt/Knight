using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCCore : MonoBehaviour
{
    private List<NPCComponents> npcCoreComponentList;

    public void AddCoreComponent(NPCComponents component)
    {
        if (npcCoreComponentList.Contains(component))
        {
            Debug.LogError($"There is more than 1 {component} on this {gameObject.name}");
            return;
        }
        
        npcCoreComponentList.Add(component);
    }
    
    public T GetCoreComponent<T>() where T : NPCComponents
    {
        var component = npcCoreComponentList.OfType<T>().FirstOrDefault();

        if (component == null)
        {
            Debug.LogError($"There is no component {typeof(T)} on this {gameObject.name}");
        }

        return component;
    }

    public NPCComponents GetFirstComponent()
    {
        return npcCoreComponentList.FirstOrDefault();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractableArea : MonoBehaviour, IInteractable
{
    public abstract void Interact();
}

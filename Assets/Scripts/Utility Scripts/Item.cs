using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public Sprite itemIcon;
    public int amount;

    public void Interact(SC_FP_Shooter7 player)
    {
        player.AddToInventory(item:this);

    }
}

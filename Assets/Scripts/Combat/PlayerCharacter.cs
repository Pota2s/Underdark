using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerCharacter : CharacterObject
{
    public List<Item> slots;
    override protected void Start()
    {
        base.Start();
        slots = new List<Item>(capacity:2);
    }

    public void AddItem(Item item)
    {
        slots.Add(item);
    }
}

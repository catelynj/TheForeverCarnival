using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string name;
    //public Sprite icon;
    public string description;
    public ItemType itemType;

    public virtual void Use()
    {
        Debug.Log(name + " was used.");
    }
    public enum ItemType { Dart, Ball }
}

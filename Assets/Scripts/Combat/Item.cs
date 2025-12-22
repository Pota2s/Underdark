using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Category("Item Info")]
    public string itemSingular;
    public string itemPlural;
    public string description;
    [Category("Item Properties")]
    public int maxStackSize = 1;
    public Sprite icon;
    public PartialStatistics statModifiers;
}

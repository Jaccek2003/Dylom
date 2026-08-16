using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName;

    public string ItemName
    {
        get => itemName;
    }

    [SerializeField]
    private Sprite icon;

    public Sprite Icon
    {
        get => icon;
    }

    public override bool Equals(object obj)
    {
        if (obj is not Item other)
            return false;

        return itemName == other.itemName &&
               icon == other.icon;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(itemName, icon);
    }
}

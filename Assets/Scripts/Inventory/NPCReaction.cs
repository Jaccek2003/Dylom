using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReaction : MonoBehaviour
{
    public void ReactToItem(string itemName)
    {
        Debug.Log($"NPC: Dzi�kuj� za {itemName}!");
        // Mo�esz doda� tutaj dodatkowe reakcje, np. odblokowanie drzwi, dialog itp.
    }
}

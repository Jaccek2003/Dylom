using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCReaction : MonoBehaviour
{
    public void ReactToItem(string itemName)
    {
        Debug.Log($"NPC: Dziêkujê za {itemName}!");
        // Mo¿esz dodaæ tutaj dodatkowe reakcje, np. odblokowanie drzwi, dialog itp.
    }
}

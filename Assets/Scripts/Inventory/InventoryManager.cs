using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private List<string> inventory = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Dodaje przedmiot do inwentarza.
    /// </summary>
    /// <param name="itemName">Nazwa przedmiotu.</param>
    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log($"Dodano przedmiot: {itemName}");
    }

    /// <summary>
    /// Usuwa przedmiot z inwentarza, je�li istnieje.
    /// </summary>
    /// <param name="itemName">Nazwa przedmiotu do usuni�cia.</param>
    public void RemoveItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"Usuni�to przedmiot: {itemName}");
        }
        else
        {
            Debug.LogWarning($"Przedmiot '{itemName}' nie istnieje w inwentarzu.");
        }
    }

    /// <summary>
    /// Usuwa konkretny przedmiot z inwentarza, je�li istnieje.
    /// </summary>
    /// <param name="itemName">Nazwa konkretnego przedmiotu.</param>
    public void RemoveSpecificItem(string itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
            Debug.Log($"Przedmiot '{itemName}' zosta� usuni�ty z inwentarza.");
        }
        else
        {
            Debug.LogWarning($"Przedmiot '{itemName}' nie znajduje si� w inwentarzu i nie mo�na go usun��.");
        }

        ShowInventory(); // Wy�wietl aktualn� zawarto�� inwentarza (opcjonalnie)
    }

    /// <summary>
    /// Sprawdza, czy inwentarz zawiera przedmiot.
    /// </summary>
    /// <param name="itemName">Nazwa przedmiotu do sprawdzenia.</param>
    /// <returns>True, je�li przedmiot istnieje w inwentarzu; w przeciwnym razie false.</returns>
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    /// <summary>
    /// Wy�wietla aktualn� zawarto�� inwentarza w konsoli.
    /// </summary>
    public void ShowInventory()
    {
        Debug.Log("Zawarto�� inwentarza:");
        if (inventory.Count == 0)
        {
            Debug.Log("- Inwentarz jest pusty.");
            return;
        }

        foreach (var item in inventory)
        {
            Debug.Log($"- {item}");
        }
    }
}

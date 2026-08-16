using System.Collections.Generic;
using UnityEngine;
public class Reakcja_Dialog
{
    public Character character; // Postaæ mówi¹ca (ikona, imiê)
    [TextArea] public string line; // Tekst dialogowy
}

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogi : ScriptableObject
{
    public List<DialogueLine> dialogueLines; // Lista linii dialogowych
}

[System.Serializable]
public class Character
{
    public string name; // Imiê postaci
    public Sprite icon; // Ikona postaci
}

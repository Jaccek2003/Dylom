using System.Collections.Generic;
using UnityEngine;
public class Reakcja_Dialog
{
    public Character character; // Posta� m�wi�ca (ikona, imi�)
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
    public string name; // Imi� postaci
    public Sprite icon; // Ikona postaci
}

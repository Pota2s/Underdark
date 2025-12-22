using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{

    public static CharacterCreation instance;
    public event Action<CharacterStatistics> CharacterChanged;

    [SerializeField]
    private Text descriptionTextBox;
    private CharacterStatistics currentCharacterClass;
    [SerializeField]
    private Text characterName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        currentCharacterClass = null;
    }

    public void ChangeCharacter(CharacterStatistics classBaseStat)
    {

        CharacterChanged?.Invoke(classBaseStat);
        if (classBaseStat != null)
        {
            Debug.Log("Character changed to: " + classBaseStat.className);
            currentCharacterClass = classBaseStat;
            descriptionTextBox.text = classBaseStat.GetDescription();
        } else
        {
            Debug.Log("Character cleared.");
            descriptionTextBox.text = "Select a class to see its description.";
        }

    }
    
    public bool CreateCharacterData(int index)
    {
        Debug.Log($"Creating character { characterName.text } as a { currentCharacterClass.className }");
        bool success = PartyManager.instance.CreatePlayerCharacter(currentCharacterClass, index, characterName.text);
        
        if (!success)
        {
            Debug.Log("Failed to create character. Invalid Index");
            return false;
        }
        
        Debug.Log("Character created: " + currentCharacterClass.className);
        currentCharacterClass = null;
        characterName.text = "";

        ChangeCharacter(null);

        return true;
    }
}

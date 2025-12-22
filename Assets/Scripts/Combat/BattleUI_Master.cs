using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using JetBrains.Annotations;

public class BattleUI_Master : MonoBehaviour
{
    public static BattleUI_Master instance;

    [SerializeField] private GameObject spellContainer;
    [SerializeField] private GameObject targettingContainer;
    [SerializeField] private TargettingButton TargettingButtonPrefab;
    private List<TargettingButton> targettingButtons = new List<TargettingButton>();

    public Button attackButton;
    public Button spellButton;
    public Button itemButton;
    public Button defendButton;
    
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Enable()
    {

        Debug.Log("Battle UI Enabled");

        attackButton.interactable = true;
        spellButton.interactable = true;
        //itemButton.interactable = true; // Not implemented yet
        //defendButton.interactable = true; // Not implemented yet
    }


    public void Disable()
    {
        Debug.Log("Battle UI Disabled");

        attackButton.interactable = false;
        spellButton.interactable = false;
        itemButton.interactable = false;
        defendButton.interactable = false;
    }

    public void Setup()
    {
        spellContainer.SetActive(false);
        targettingContainer.SetActive(false);

        List<CharacterObject> characterObjects = Stage.instance.GetPlayerParty();
        characterObjects.AddRange(Stage.instance.GetEnemyParty());

        foreach(CharacterObject character in characterObjects) { 
            TargettingButton targettingButton = Instantiate(TargettingButtonPrefab);
            
            
            targettingButton.transform.SetParent(targettingContainer.transform, false);
            targettingButton.SetTarget(character);
            targettingButton.SetEnabled(false);
            if (character.characterData != null) { 
                targettingButton.SetText(character.characterData.characterName);
            } else
            {
                targettingButton.SetText("Empty");
            }
        }

    }

    public void EnableSpellSelection(CharacterObject actor, Action<Action_Base> onSpellSelected)
    {
        spellContainer.SetActive(true);
        BattleUI_Spell spellSelectionUI = spellContainer.GetComponent<BattleUI_Spell>();
        spellSelectionUI.Populate(actor, (Action_Base selectedSpell) =>
        {
            spellContainer.SetActive(false);
            onSpellSelected.Invoke(selectedSpell);
        });
    }

    public void EnableTargeting(List<CharacterObject> possibleTargets, Action<CharacterObject> onTargetSelected)
    {
        targettingContainer.SetActive(true);
        foreach (TargettingButton button in targettingButtons)
        {
            if (possibleTargets.Contains(button.GetTarget()))
            {
                Button buttonComponent = button.GetComponent<Button>();
                button.SetEnabled(true);
                button.Bind(onTargetSelected);
            }
            else
            {
                button.SetEnabled(false);
            }
        }

        
    }

    public void DisableTargeting()
    {
        targettingContainer.SetActive(false);
        foreach (TargettingButton button in targettingButtons)
        {
            button.SetEnabled(false);
            button.Unbind();
        }
    }
}

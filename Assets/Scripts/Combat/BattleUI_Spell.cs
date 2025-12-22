using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
public class BattleUI_Spell : MonoBehaviour
{
    [SerializeField] private SpellButton spellButtonPrefab;
    public void Populate(CharacterObject character, Action<Action_Base> action)
    {
        List<Action_Base> spells = character.characterData.moves;

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        spells.RemoveAt(0); // Remove basic attack from spell list
        foreach (Action_Base spell in spells)
        {
            SpellButton spellButton = Instantiate(spellButtonPrefab);
            spellButton.transform.SetParent(transform, false);
            
        }
    }
}

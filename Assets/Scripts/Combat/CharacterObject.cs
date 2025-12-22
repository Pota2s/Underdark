using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public CharacterData characterData;
    [SerializeField]
    private CharacterStatistics dataOverride;
    public ActionSelector actionSelector;

    virtual protected void Start()
    {

    }

    public IEnumerator TakeTurn(BattleContext ctx)
    {
        if (characterData.IsDead())
        {
            yield break;
        }

        Action_Base selectedAction = null;
        List<CharacterObject> selectedTargets = null;

        yield return StartCoroutine(actionSelector.SelectAction(this, ctx, (action, targets) =>
        {
            selectedAction = action;
            selectedTargets = targets;
        }));

        if (selectedAction != null ||
            selectedTargets != null)
        { 
            selectedAction.Perform(this, selectedTargets);
        }

    }

    public void InitializeCharacter(CharacterData existingData, ActionSelector selector)
    {
        actionSelector = selector;
        if (dataOverride != null)
        {
            characterData = new CharacterData(dataOverride);
            characterData.characterName = dataOverride.className;
            characterData.characterDescription = dataOverride.classDescription;
            characterData.baseStatistics = dataOverride;
            return;
        }
        characterData = existingData;
    }

    public int GetPhysicalDamage()
    {
        return characterData.GetPhysicalDamage();
    }

    public int GetMagicalDamage()
    {
        return characterData.GetMagicalDamage();
    }

    public int GetPhysicalDefense()
    {
        return characterData.GetPhysicalDefense();
    }

    public float GetMagicalDefense()
    {
        return characterData.GetMagicalDefense();
    }

    public bool RestoreMana(int amount)
    {
        return characterData.RestoreMana(amount);
    }

    public bool RestoreHealth(int amount)
    {
        return characterData.RestoreHealth(amount);
    }

    public void TakePhysicalDamage(int amount)
    {
        characterData.TakePhysicalDamage(amount);
    }

    public void TakeMagicalDamage(int amount)
    {
        characterData.TakeMagicalDamage(amount);
    }

    public int GetSpeed()
    {
        return characterData.GetSpeed();
    }

    public bool UseMana(int amount)
    {
        return characterData.UseMana(amount);
    }

}

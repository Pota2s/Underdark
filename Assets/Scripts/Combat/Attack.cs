using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Action/Attack")]
public class Attack : Action_Base
{
    public float physicalMultiplier;
    public int physicalFlatDamage;

    public float magicalMultiplier;
    public int magicalFlatDamage;
    public int manaCost;

    public override void Perform(CharacterObject performer, List<CharacterObject> targets)
    {
        int physicalDamage = Mathf.FloorToInt(performer.GetPhysicalDamage() * physicalMultiplier) + physicalFlatDamage;
        int magicalDamage = Mathf.FloorToInt(performer.GetMagicalDamage() * magicalMultiplier) + magicalFlatDamage;
        bool canCast = performer.UseMana(manaCost);

        if (physicalDamage > 0)
        {
            foreach (CharacterObject t in targets)
            {
                t.TakePhysicalDamage(physicalDamage);
                
            }
        }

        if (magicalDamage > 0 && canCast)
        { 
            foreach (CharacterObject t in targets)
            {
                t.TakeMagicalDamage(magicalDamage);
            }
        }
    }

    public override string GetName()
    {
        return actionName;
    }

    public override string GetDescription()
    {
        string desc = actionDescription + "\n";

        if (physicalMultiplier > 0 || physicalFlatDamage > 0)
        {
            desc += $"Physical Damage: {physicalMultiplier * 100}% of Attack";
            if (physicalFlatDamage > 0)
            {
                desc += $" + {physicalFlatDamage}";
            }
            desc += "\n";
        }

        if (magicalMultiplier > 0 || magicalFlatDamage > 0)
        {
            desc += $"Magical Damage: {magicalMultiplier * 100}% of Affinity";
            if (magicalFlatDamage > 0)
            {
                desc += $" + {magicalFlatDamage}";
            }
            desc += "\n";
        }

        if (manaCost > 0)
        {
            desc += $"Mana Cost: {manaCost}\n";
        }

        return desc;
    }
}

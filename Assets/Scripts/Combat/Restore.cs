using UnityEngine;
[CreateAssetMenu(fileName = "Restore", menuName = "Scriptable Objects/Action/Restore")]

public class Restore : Action_Base
{
    public int scalingHealthRestore;
    public int flatHealthRestore;

    public int scalingManaRestore;
    public int flatManaRestore;
    public float affinityMultiplier = 0.5f;
    public override void Perform(CharacterObject performer, System.Collections.Generic.List<CharacterObject> target)
    {
        // Calculate restores
        int healthRestore = Mathf.RoundToInt(scalingHealthRestore * (performer.GetMagicalDamage() * affinityMultiplier)) + flatHealthRestore;
        int manaRestore = Mathf.RoundToInt(scalingManaRestore * (performer.GetMagicalDamage() * affinityMultiplier)) + flatManaRestore;

        foreach (CharacterObject t in target)
        {
            if (healthRestore > 0)
            {
                t.RestoreHealth(healthRestore);
            }
            if (manaRestore > 0)
            {
                t.RestoreMana(manaRestore);
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

        if (scalingHealthRestore > 0 || flatHealthRestore > 0)
        {
            desc += $"Health Restoration : {scalingHealthRestore * 100}% of Affinity";
            if (flatHealthRestore > 0)
            {
                desc += $" + {flatHealthRestore}";
            }

            desc += "\n";
        }

        if (scalingManaRestore > 0 || flatManaRestore > 0)
        {
            desc += $"Mana Restoration : {scalingHealthRestore * 100}% of Affinity";
            if (flatManaRestore > 0)
            {
                desc += $" + {flatManaRestore}";
            }

            desc += "\n";
        }

        return desc;
    }
}

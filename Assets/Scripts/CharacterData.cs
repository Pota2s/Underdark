using System.Collections.Generic;
using UnityEngine;

public class CharacterData : IDescribeable
{
    public string characterName;
    public CharacterStatistics baseStatistics;
    public string characterDescription;

    public int maxHealthPoints;
    public int healthPoints;
    public int attack;
    public int defense;

    public int maxManaPoints;
    public int manaPoints;
    public int affinity;
    public int resistance;

    public int speed;
    public int luck;
    
    public int level;
    public int experience;
    public int experienceToNextLevel;

    public List<Action_Base> moves;
    public List<Effect> statuses;

    

    public CharacterData(CharacterStatistics data, string name)
    {
        baseStatistics = data;
        characterName = name;
        characterDescription = data.classDescription;
        
        maxHealthPoints = data.healthPoints;
        healthPoints = data.healthPoints;
        attack = data.attack;
        defense = data.defense;
        
        maxManaPoints = data.manaPoints;
        manaPoints = data.manaPoints;
        affinity = data.affinity;
        resistance = data.resistance;
        
        speed = data.speed;
        luck = data.luck;

        level = 1;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public CharacterData(CharacterStatistics data)
    {
        baseStatistics = data;

        characterName = data.className;
        characterDescription = data.classDescription;
        maxHealthPoints = data.healthPoints;
        healthPoints = data.healthPoints;
        attack = data.attack;
        defense = data.defense;

        maxManaPoints = data.manaPoints;
        manaPoints = data.manaPoints;
        affinity = data.affinity;
        resistance = data.resistance;

        speed = data.speed;
        luck = data.luck;

        level = 1;
        experience = 0;
        experienceToNextLevel = 100;

        moves = new List<Action_Base> { data.basicAction };
        statuses = new List<Effect>();
    }

    public void CheckLevel()
    {
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            level++;

            if (baseStatistics.LevelingStats.ContainsKey(level))
            {
                PartialStatistics stats = baseStatistics.LevelingStats[level];
                maxHealthPoints += stats.healthPoints;
                attack += stats.attack;
                defense += stats.defense;
                maxManaPoints += stats.manaPoints;
                affinity += stats.affinity;
                resistance += stats.resistance;
                speed += stats.speed;
                luck += stats.luck;
                healthPoints = maxHealthPoints;
                manaPoints = maxManaPoints;
            }

            if (baseStatistics.LevelingActions.ContainsKey(level))
            {
                List<Action_Base> newActions = baseStatistics.LevelingActions[level];
                foreach (Action_Base action in newActions)
                {
                    if (!moves.Contains(action))
                    {
                        moves.Add(action);
                    }
                }
            }
        }
    }
    public int GetPhysicalDamage()
    {
        return Mathf.CeilToInt(Random.Range((float)attack, (float)attack * 1.2f));
    }

    public int GetMagicalDamage()
    {
        return Mathf.CeilToInt(Random.Range((float)affinity * 0.7f, (float)affinity * 1.5f));
    }

    public int GetPhysicalDefense()
    {
        return defense;
    }


    public float GetMagicalDefense()
    {
        return (float)resistance / (25f + (float)resistance);
    }
    public int GetSpeed()
    {
        return speed;
    }

    public float GetCriticalChance()
    {
        return luck / (luck + 20f) * 100f;
    }
    public bool RollCritical()
    {
        return Random.Range(1f,100f) < GetCriticalChance();
    }

    public void TakePhysicalDamage(int damage)
    {
        damage -= GetPhysicalDefense();
        healthPoints -= Mathf.Max(damage,0);
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);
    }

    public void TakeMagicalDamage(int damage)
    {
        damage = Mathf.CeilToInt((float)damage * (1f - GetMagicalDefense()));
        healthPoints -= Mathf.CeilToInt(damage);
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);
    }

    public bool IsDead()
    {
        return healthPoints <= 0;
    }

    public bool RestoreHealth(int amount)
    {
        if (healthPoints < maxHealthPoints)
        {
            healthPoints += amount;
            healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);
            return true;
        }
        return false;
    }

    public bool RestoreMana(int amount)
    {
        if (manaPoints < maxManaPoints)
        {
            manaPoints += amount;
            manaPoints = Mathf.Clamp(manaPoints, 0, maxManaPoints);
            return true;
        }
        return false;
    }

    public bool UseMana(int amount)
    {
        if (manaPoints >= amount || manaPoints == maxManaPoints)
        {
            manaPoints -= amount;
            manaPoints = Mathf.Clamp(manaPoints, 0, maxManaPoints);
            return true;
        }
        return false;
    }

    public string GetName()
    {
        return characterName;
    }

    public string GetDescription()
    {
        string desc = characterDescription + "\n\n";
        desc += "HP : " + healthPoints + "/" +  maxHealthPoints + "\n";
        desc += "MP : " + manaPoints + "/" +  maxManaPoints + "\n\n";
        desc += "ATK: " + attack + "\n";
        desc += "DEF: " + defense + "\n\n";
        desc += "AFF: " + affinity + "\n";
        desc += "RES: " + resistance + "\n\n";
        desc += "SPD: " + speed + "\n";
        desc += "LCK: " + luck + "\n";
        return desc;
    }
}

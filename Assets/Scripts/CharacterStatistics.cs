using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct PartialStatistics
{
    public int healthPoints;
    public int attack;
    public int defense;

    public int manaPoints;
    public int affinity;
    public int resistance;

    public int speed;
    public int luck;
}
[System.Serializable]
public class LevelStatsEntry
{
    public int level;
    public PartialStatistics stats;
}

[System.Serializable]
public class LevelActionsEntry
{
    public int level;
    public List<Action_Base> actions;
}

[CreateAssetMenu(fileName = "BaseStatistics", menuName = "Scriptable Objects/BaseStatistics")]
public class CharacterStatistics : ScriptableObject, IDescribeable
{
    

    [Header("Class Info")]
    public string className;
    [TextArea]
    public string classDescription;
    public Sprite classSprite;

    [Header("Primary Stats")]
    public int healthPoints;
    public int attack;
    public int defense;

    public int manaPoints;
    public int affinity;
    public int resistance;

    public int speed;
    public int luck;

    public Action_Base basicAction;

    public List<LevelStatsEntry> statsPerLevel = new List<LevelStatsEntry>();
    public List<LevelActionsEntry> actionsPerLevel = new List<LevelActionsEntry>();

    public Dictionary<int, PartialStatistics> LevelingStats { get; private set; }
    public Dictionary<int, List<Action_Base>> LevelingActions { get; private set; }

    public void OnEnable()
    {
        LevelingStats = new Dictionary<int, PartialStatistics>();
        LevelingActions = new Dictionary<int, List<Action_Base>>();
        
        foreach (var entry in statsPerLevel)
        {
            LevelingStats[entry.level] = entry.stats;
        }

        foreach (var entry in actionsPerLevel)
        {
            LevelingActions[entry.level] = entry.actions;
        }
    }

    public Dictionary<string,int> Serialize()
    {
        Dictionary<string, int> data = new Dictionary<string, int>
        {
            { "healthPoints", healthPoints },
            { "attack", attack },
            { "defense", defense },
            { "manaPoints", manaPoints },
            { "affinity", affinity },
            { "resistance", resistance },
            { "speed", speed },
            { "luck", luck }
        };

        return data;
    }

    public string GetDescription()
    {
        string description = classDescription + "\n\n" +
                             "Health Points: " + healthPoints + "\n" +
                             "Attack: " + attack + "\n" +
                             "Defense: " + defense + "\n\n" +
                             "Mana Points: " + manaPoints + "\n" +
                             "Affinity: " + affinity + "\n" +
                             "Resistance: " + resistance + "\n\n" +
                             "Speed: " + speed + "\n" +
                             "Luck: " + luck;
        return description;
    }

    public string GetName()
    {
        return className;
    }
}

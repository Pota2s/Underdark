using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "Action")]

public abstract class Action_Base : ScriptableObject, IDescribeable
{
    public enum TargetType
    {
        SELF,
        ALLY,
        ENEMY,
        ALL_ALLIES,
        ALL_ENEMIES,
        EVERYONE
    }

    public enum ActionType
    {
        PHYSICAL,
        MAGICAL,
        SUPPORT
    }

    public TargetType target;
    public ActionType actionType;

    public string actionName;
    public string actionDescription;

    public List<Effect> physicalEffects;
    public List<Effect> magicalEffects;
    abstract public void Perform([NotNull] CharacterObject performer, [NotNull] List<CharacterObject> targets);
    abstract public string GetName();
    abstract public string GetDescription();


}

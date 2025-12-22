using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Scriptable Objects/Effect")]
public class Effect : ScriptableObject
{
    [Header("Multipliers")]
    public float attackMultiplier = 1f;
    public float defenseMultiplier = 1f;

    public float affinityMultiplier = 1f;
    public float resistanceMultiplier = 1f;

    public float speedMultiplier = 1f;
    public float luckMultiplier = 1f;

    [Header("Flat Modifiers")]
    public int duration;
    public int healthOverTime;
    public int manaOverTime;

    public int flatAttack;
    public int flatDefense;

    public int flatAffinity;
    public int flatResistance;

    public int flatSpeed;
    public int flatLuck;
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public struct BattleContext
{
    public List<CharacterObject> playerCharacters;
    public List<CharacterObject> enemyCharacters;

    public BattleContext(List<CharacterObject> players, List<CharacterObject> enemies)
    {
        playerCharacters = players;
        enemyCharacters = enemies;
    }
}

public class Stage : MonoBehaviour
{
    [SerializeField] private CharacterStatistics testEnemyData;
    public static Stage instance;

    public CharacterObject characterObjectPrefab;

    public List<CharacterObject> playerObjects;
    public List<CharacterObject> enemyObjects;

    public List<CharacterObject> turnOrder;

    public CharacterObject selectedCharacter;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        Setup();
        StartCoroutine(TurnManager());
    }

    private void Setup()
    {

        playerObjects = new List<CharacterObject>();

        foreach (CharacterData data in PartyManager.instance.GetPlayerParty())
        {
            CharacterObject p = Instantiate(characterObjectPrefab);
            p.InitializeCharacter(data,new PlayerActionSelector());
            playerObjects.Add(p);
        }

        enemyObjects = new List<CharacterObject>();

        for(int i = 0; i < PartyManager.instance.GetEnemyParty().Count;i++)
        {
            CharacterObject p = Instantiate(characterObjectPrefab);
            p.InitializeCharacter(new CharacterData(testEnemyData), new DumbEnemyActionSelector());
            enemyObjects.Add(p);
        }

        turnOrder = new List<CharacterObject>();
        turnOrder.AddRange(playerObjects);
        turnOrder.AddRange(enemyObjects);

        selectedCharacter = null;

        BattleUI_Master.instance.Setup();
    }

    private IEnumerator TurnManager()
    {
        turnOrder = new List<CharacterObject>();
        turnOrder.AddRange(playerObjects);
        turnOrder.AddRange(enemyObjects);

        turnOrder.Sort((a, b) => b.GetSpeed().CompareTo(a.GetSpeed()));
        while (true)
        {
            foreach(CharacterObject character in turnOrder)
            {
                BattleContext ctx = new BattleContext(playerObjects, enemyObjects);

                DescriptionManager.instance.SetDescription(character.characterData);
                yield return character.TakeTurn(ctx);
            }

        }
    }

    public List<CharacterObject> GetPlayerParty()
    {
        return playerObjects;
    }
    public List<CharacterObject> GetEnemyParty()
    {
        return enemyObjects;
    }

    public CharacterObject GetPlayerCharacter(int index)
    {
        if (index >= 0 && index < playerObjects.Count)
        {
            return playerObjects[index];
        }
        return null;
    }

    public CharacterObject GetEnemyCharacter(int index)
    {
        if (index >= 0 && index < enemyObjects.Count)
        {
            return enemyObjects[index];
        }
        return null;
    }


}

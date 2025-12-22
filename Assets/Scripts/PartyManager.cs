using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager instance;

    public Party playerParty;
    public Party enemyParty;
    
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        playerParty = new Party();
        enemyParty = new Party();


        DontDestroyOnLoad(gameObject);
    }

    public bool CreateEnemyCharacter(CharacterStatistics data, int index)
    {
        if (index < 0 || index >= 4)
        {
            return false;
        }
        enemyParty.SetMember(index, new CharacterData(data));
        return true;
    }

    public bool CreatePlayerCharacter(CharacterStatistics data, int index, string name)
    {
        if (index < 0 || index >= 4)
        {
            return false;
        }
        playerParty.SetMember(index,new CharacterData(data,name));
        return true;
    }

    public void SetPlayerParty(Party party)
    {
        playerParty = party;
    }

    public Party GetPlayerParty()
    {
        return playerParty;
    }

    public Party GetEnemyParty()
    {
        return enemyParty;
    }

}

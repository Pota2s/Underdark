using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{

    private Button button;
    private Image image;
    private Text text;

    public int index;
    
    void Awake()
    {

        button = transform.GetComponent<Button>();
        image = transform.GetChild(1).GetComponent<Image>();
        text = transform.GetComponentInChildren<Text>();
    }
    void Start()
    {
        image.enabled = false;
        image.sprite = null;

        text.text = $"Slot #0";
        button.onClick.AddListener(OnClicked);
    }

    public void SetIndex(int i)
    {
        index = i;
        text.text = $"Slot #{index + 1}";
    }

    void OnClicked()
    {
        if (CharacterCreation.instance == null)
        {
            Debug.LogWarning("CharacterCreation instance is null.");
            return;
        }

        bool success = CharacterCreation.instance.CreateCharacterData(index);

        CharacterData c = PartyManager.instance.playerParty.GetMember(index);

        if (!success)
        {
            return;
        }

        if (c != null)
        {
            Debug.Log("Character Slot " + index + " filled with " + c.characterName);
            image.enabled = true;
            image.sprite = c.baseStatistics.classSprite;
            text.text = c.characterName;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ClassSelection : MonoBehaviour
{
    public CharacterStatistics classBaseStat;
    private Text buttonText;
    private Button button;

    private void Awake()
    {
        buttonText = transform.GetChild(0).GetComponent<Text>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClicked);
        UpdateName();

        CharacterCreation.instance.CharacterChanged += (newClass) =>
        {
            if (newClass == classBaseStat)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        };

    }
    
    private void UpdateName()
    {
        if (classBaseStat != null)
        {
            buttonText.text = classBaseStat.GetName();
        }
    }

    public void SetClass(CharacterStatistics newClass)
    {
        classBaseStat = newClass;
        UpdateName();
    }
    private void OnClicked()
    {
        CharacterCreation.instance.ChangeCharacter(classBaseStat);
    }

}

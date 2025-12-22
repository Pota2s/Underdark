using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreation_Confirm : MonoBehaviour
{
    public void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnConfirmButtonPressed);
    }

    public void OnConfirmButtonPressed()
    {
        Party playerParty = PartyManager.instance.GetPlayerParty();

        if (playerParty.IsEmpty())
        {
            Debug.LogWarning("Cannot start combat with an empty party.");
            return;
        }

        SceneManager.LoadScene("CombatScene");
    }
}

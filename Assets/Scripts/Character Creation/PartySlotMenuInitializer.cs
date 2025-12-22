using UnityEngine;

public class PartySlotMenuInitializer : MonoBehaviour
{
    [SerializeField]
    private CharacterSlot characterSlotPrefab;

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            CharacterSlot slot = Instantiate(characterSlotPrefab, transform);
            slot.SetIndex(i);
        }
    }
}

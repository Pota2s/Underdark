using UnityEngine;
using UnityEngine.UI;

public class DescriptionManager : MonoBehaviour
{
    public static DescriptionManager instance;

    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    void Awake()
    {
        if( instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void SetDescription(IDescribeable describeable)
    {
        nameText.text = describeable.GetName();
        descriptionText.text = describeable.GetDescription();
    }
}

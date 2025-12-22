using System;
using UnityEngine;
using UnityEngine.UI;

public class TargettingButton : MonoBehaviour
{
    [SerializeField] private Text text;
    private CharacterObject target;
    


    public void SetText(string buttonText)
    {
        text.text = buttonText;
    }

    public void SetTarget(CharacterObject character)
    {
        target = character;
    }

    public void SetEnabled(bool value)
    {
        GetComponent<Button>().interactable = value;
    }

    public CharacterObject GetTarget()
    {
        return target;
    }

    public void Bind(Action<CharacterObject> action)
    {
        Button buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.RemoveAllListeners();
        buttonComponent.onClick.AddListener(() => action.Invoke(target));
    }

    public void Unbind()
    {
        Button buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.RemoveAllListeners();
    }

}

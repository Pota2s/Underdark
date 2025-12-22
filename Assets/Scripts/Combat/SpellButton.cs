using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    private Text text;
    private Button button;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
    }

    public void SetText(string spellName)
    {
        text.text = spellName;
    }
    public void SetEnabled(bool enabled)
    { 
        button.interactable = enabled;
    }

    public void Bind(Action_Base spell,Action<Action_Base> action)
    {
        SetText(spell.GetName());
        SetEnabled(true);
        button.onClick.AddListener(() =>
        {
            action.Invoke(spell);
        });
    }
}

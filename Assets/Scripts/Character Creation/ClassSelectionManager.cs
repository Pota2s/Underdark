using UnityEngine;
using System.Collections.Generic;

public class ClassSelectionManager : MonoBehaviour
{
    [SerializeField]
    private ClassSelection classSelectionPrefab;
    public List<CharacterStatistics> classes;

    void Start()
    {
        foreach (CharacterStatistics classStat in classes)
        {
            ClassSelection classSelectionInstance = Instantiate(classSelectionPrefab, transform);
            classSelectionInstance.SetClass(classStat);
            classSelectionInstance.transform.SetParent(transform);
        }
    }
}

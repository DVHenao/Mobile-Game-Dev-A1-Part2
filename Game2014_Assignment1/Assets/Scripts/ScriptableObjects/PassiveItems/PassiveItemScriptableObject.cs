using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multiplier;
    public float Multiplier {  get => multiplier; private set => multiplier = value; }
    // Start is called before the first frame update


    [SerializeField]
    int level; //not meant to be modified in game only in editor
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab; // the prefab of the upgraded object
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon; // the prefab of the upgraded object
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField]
    new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField]
    string description;
    public string Description { get => description; private set => description = value; }


}

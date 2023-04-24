using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 0)]
public class Character : ScriptableObject
{
    public enum Characters
    {
        SousChefX,
        SousChefY,
    }
    public Characters character;
    public float completionTime;
}

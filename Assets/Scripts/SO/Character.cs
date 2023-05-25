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
    [Tooltip("Delta value that determine how fast until the progress bar becomes inactive (turns to color grey)")]
    [Range(0f, 1f)] public float characteristicDeltaValue;
}

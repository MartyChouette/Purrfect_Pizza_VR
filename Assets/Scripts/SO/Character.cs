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
    public Color[] progressBarColors;
    public float characteristicScaler;
    [HideInInspector] public int numberOfPhases;

    private void OnValidate()
    {
        if (progressBarColors != null)
        {
            numberOfPhases = progressBarColors.Length;
        }
        else
        {
            numberOfPhases = 0;
        }
    }
}

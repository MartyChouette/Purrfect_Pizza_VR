using UnityEngine;

[CreateAssetMenu(fileName = "New Pizza", menuName = "Pizza")]
public class Pizza : ScriptableObject
{
    public GameObject doughPrefab;
    public GameObject[] toppingPrefabs;
}

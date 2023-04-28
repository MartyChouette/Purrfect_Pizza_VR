using UnityEngine;

[CreateAssetMenu(fileName = "New Level Score", menuName = "Score")]
public class Score : ScriptableObject
{
    public float goal;
    [HideInInspector] public float currentScore;

    private void Start()
    {
        currentScore = 0;
    }

    private void OnDisable()
    {
        currentScore = 0;
    }
}

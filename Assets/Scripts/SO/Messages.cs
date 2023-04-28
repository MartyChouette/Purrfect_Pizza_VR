using UnityEngine;

[CreateAssetMenu(fileName = "New Messages", menuName = "Messages")]
public class Messages : ScriptableObject
{
    public string[] correctOrderMessages;
    public string[] wrongOrderMessages;
}

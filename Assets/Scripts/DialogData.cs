using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dialog", fileName = "Dialog")]
public class DialogData : ScriptableObject
{
    [SerializeField] private bool isFirstSideLeft;
    [SerializeField] private Line[] lines;

    public Line[] Lines => lines;
    public bool IsFirstSideLeft => isFirstSideLeft;

    [System.Serializable]
    public struct Line
    {
        public Character character;
        [TextArea] public string message;
    }
}

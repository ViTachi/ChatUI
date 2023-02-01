using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character", fileName = "Character")]
public class Character : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private Sprite chatIcon;

    public string CharacterName => characterName;
    public Sprite ChatIcon => chatIcon;
}

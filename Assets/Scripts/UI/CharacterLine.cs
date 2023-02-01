using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLine : MonoBehaviour
{
    [SerializeField] private Image characterIconImage;
    [SerializeField] private Text dialogText;
    [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] private int sidePadding;

    public void SetText(string text)
    {
        dialogText.text = text;
    }

    public void SetIcon(Sprite icon)
    {
        characterIconImage.sprite = icon;
    }

    public void Show(bool onLeft = false)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);

        if(onLeft)
        {
            horizontalLayoutGroup.reverseArrangement = true;
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            horizontalLayoutGroup.padding.right = sidePadding;
            horizontalLayoutGroup.padding.left = 0;
            characterIconImage.transform.localScale = Vector3.one;
        }
        else
        {
            horizontalLayoutGroup.reverseArrangement = false;
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleRight;
            horizontalLayoutGroup.padding.left = sidePadding;
            horizontalLayoutGroup.padding.right = 0;
            characterIconImage.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

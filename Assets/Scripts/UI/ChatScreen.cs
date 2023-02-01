using DG.Tweening;
using System;
using UnityEngine;

public class ChatScreen : MonoBehaviour, IScreenElement
{
    [SerializeField] private GameObject characterLinePrefab;
    [SerializeField] private RectTransform linesParent;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float scrollSpeed;

    private Tweener alphaTweener;

    private int currentLineIndex = 0;
    private Character lastCharacter;
    private bool isCurrentSideLeft;

    private DialogData currentDialogData;
    private Action OnComplete;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    public void Show(bool immediately = false)
    {
        gameObject.SetActive(true);
        alphaTweener?.Kill();

        if (immediately)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            alphaTweener = canvasGroup.DOFade(1, 0.4f);
        }      
    }

    public void Hide(bool immediately = false)
    {
        if (immediately)
        {
            gameObject.SetActive(false);
        }
        else
        {
            alphaTweener = canvasGroup.DOFade(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void StartDialog(DialogData dialogData, Action OnComplete = null)
    {
        currentDialogData = dialogData;
        currentLineIndex = -1;
        lastCharacter = null;
        isCurrentSideLeft = dialogData.IsFirstSideLeft;
        this.OnComplete = OnComplete;

        ShowNextLine();
    }

    public void EndDialog()
    {
        OnComplete?.Invoke();
    }

    public void ShowNextLine()
    {
        currentLineIndex++;
        if(currentLineIndex >= currentDialogData.Lines.Length)
        {
            EndDialog();
            return;
        }

        DialogData.Line currentLine = currentDialogData.Lines[currentLineIndex];

        if (lastCharacter != null && lastCharacter != currentLine.character)
        {
            isCurrentSideLeft = !isCurrentSideLeft;
        }

        lastCharacter = currentLine.character;

        CreateLine(currentLine);
    }

    private void CreateLine(DialogData.Line line)
    {
        CharacterLine characterLine = Instantiate(characterLinePrefab, linesParent).GetComponent<CharacterLine>();
        characterLine.SetText(line.message);
        characterLine.SetIcon(line.character.ChatIcon);
        characterLine.Show(isCurrentSideLeft);
    }

    private void HandleChatPosition()
    {
        if (linesParent.rect.height - 1 > rectTransform.rect.height)
        {
            linesParent.anchoredPosition = Vector2.Lerp(
                linesParent.anchoredPosition,
                new Vector2(linesParent.anchoredPosition.x, linesParent.rect.height - rectTransform.rect.height),
                scrollSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        HandleChatPosition();
    }
}

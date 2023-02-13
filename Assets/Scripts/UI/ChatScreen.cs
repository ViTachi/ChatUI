using DG.Tweening;
using System;
using UnityEngine;

public class ChatScreen : MonoBehaviour, IScreenElement
{
    [SerializeField] private CharacterLine characterLinePrefab;
    [SerializeField] private RectTransform linesParent;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float scrollSpeed;

    private int currentLineIndex = 0;
    private Character lastCharacter;
    private bool isCurrentSideLeft;

    private DialogData currentDialogData;
    private Action OnComplete;

    private RectTransform rectTransform;

    private bool isDialogProcess = false;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }

    private void OnDestroy()
    {
        DOTween.Kill(this);
    }

    public void Show(bool immediately = false)
    {
        gameObject.SetActive(true);
        DOTween.Kill(this);

        if (immediately)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.DOFade(1, 0.4f)
                .SetUpdate(true)
                .SetId(this);
        }      
    }

    public void Hide(bool immediately = false)
    {
        DOTween.Kill(this);

        if (immediately)
        {
            gameObject.SetActive(false);
        }
        else
        {
            canvasGroup.DOFade(0, 0.4f)
                .SetUpdate(true)
                .SetId(this)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void StartDialog(DialogData dialogData, Action OnComplete = null)
    {
        isDialogProcess = true;

        currentDialogData = dialogData;
        currentLineIndex = -1;
        lastCharacter = null;
        isCurrentSideLeft = dialogData.IsFirstSideLeft;
        linesParent.anchoredPosition = new Vector2(linesParent.anchoredPosition.x, 0);
        this.OnComplete = OnComplete;

        ShowNextLine();
    }

    public void EndDialog()
    {
        if (!isDialogProcess) return;

        Clear();
        OnComplete?.Invoke();
        isDialogProcess = false;
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
        CharacterLine characterLine = Instantiate(characterLinePrefab, linesParent);
        characterLine.SetText(line.message);
        characterLine.SetIcon(line.character.ChatIcon);
        characterLine.Show(isCurrentSideLeft);
    }

    private void HandleChatPosition()
    {
        float targetYPosition = linesParent.rect.height - rectTransform.rect.height;

        if (targetYPosition - 1 > linesParent.anchoredPosition.y && linesParent.rect.height > rectTransform.rect.height)
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

    private void Clear()
    {
        Transform[] lines = linesParent.GetComponentsInChildren<Transform>();
        foreach (var line in lines)
        {
            if (line != linesParent) Destroy(line.gameObject);
        }
    }
}

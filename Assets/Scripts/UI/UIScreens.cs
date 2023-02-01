public class UIScreens : Singleton<UIScreens>
{
    private IScreenElement[] screenElements;

    public T GetElement<T>() where T : IScreenElement
    {
        for (int i = 0; i < screenElements.Length; i++)
        {
            if (screenElements[i] is T) return (T)screenElements[i];
        }
        return default;
    }

    public void HideAllElements(bool immediately)
    {
        foreach (var element in screenElements)
        {
            element.Hide(immediately);
        }
    }

    protected override void Setup()
    {
        screenElements = GetComponentsInChildren<IScreenElement>(true);
        HideAllElements(true);
    }
}

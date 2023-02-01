using System.Collections;
using UnityEngine;

public class Runtime : MonoBehaviour
{
    [SerializeField] private DialogData dialogData;

    private void Start()
    {
        StartCoroutine(ShowChatWithDelay());
    }

    private IEnumerator ShowChatWithDelay()
    {
        yield return new WaitForSeconds(1);
        ChatScreen chatScreen = UIScreens.Instance.GetElement<ChatScreen>();
        chatScreen.Show();
        Debug.Log("Dialog start");
        chatScreen.StartDialog(dialogData, () =>
        {
            chatScreen.Hide();
            Debug.Log("Dialog complete");
        });
    }
}

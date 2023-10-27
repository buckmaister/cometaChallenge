using TMPro;
using UnityEngine;

public class JSDataReceiver : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _textUsername;

    private string nickname;
    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("empty username because you are in editor");
        _textUsername.text = "WELCOME: " + nickname;
#else
        _textUsername.text = "WELCOME: " + nickname;
        Debug.Log("Username from js is: " + nickname);
#endif
    }

    public void SetUsername(string data)
    {
        Debug.Log("Recived from js: " + data);
        nickname = data;
    }
}
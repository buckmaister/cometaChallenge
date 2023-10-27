using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("address of addressable scene")]
    public string addrKey; 
    
    private bool alreadyShowAvatar = false;
    
    public void ShowAvatarScene()
    {
        if (!alreadyShowAvatar)
        {
            Addressables.LoadSceneAsync(addrKey, LoadSceneMode.Additive).Completed += (handle) =>
            {
                alreadyShowAvatar = true;
                Debug.Log("loaded scene correctly");
            };
        }
    }
}

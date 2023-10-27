using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private MySceneManager _mySceneManager;
    
    //manage click on "Discover your avatar" button in main scene
    public void OnClickMyAvatarBtn()
    {
        _mySceneManager.ShowAvatarScene();

    }
}

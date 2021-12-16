using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the endMenu
public class EndMenuController : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }

}

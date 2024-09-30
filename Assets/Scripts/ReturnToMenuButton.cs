using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

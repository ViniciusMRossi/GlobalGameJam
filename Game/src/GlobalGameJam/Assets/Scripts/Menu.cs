using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetButton("Pause1") && !Input.GetButton("Pause2")) return;
        SceneManager.LoadScene("Main");
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _image;

    [SerializeField]
    private GameObject _camaraControl;

    private bool enable;


    // Update is called once per frame
    void Update()
    {
        OnScreen();

    }

    private void OnScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enable)
        {
             _image.SetActive(true);
             _camaraControl.SetActive(false);
            Time.timeScale = 0;
            enable = !enable;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !enable)
        {
            _image.SetActive(false);
            _camaraControl.SetActive(true);
            Time.timeScale = 1;
            enable = !enable;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Entrega");
        Time.timeScale = 1;
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}


    

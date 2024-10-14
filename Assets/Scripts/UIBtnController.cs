using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBtnController : MonoBehaviour
{
    public Image signPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartHideGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowSignPanel()
    {
        signPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseSignPanel()
    {
        signPanel.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject quitConfirmationMenu;

    [SerializeField]
    private GameObject levelSelectionMenu;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void QuitFunction()
    {
        Application.Quit();
    }

    private void init()
    {
        Time.timeScale = 1;
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
        quitConfirmationMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
    }
}

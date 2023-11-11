using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIController : MonoBehaviour
{

    [SerializeField]
    private GameObject pausePanel;

    public static bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    private void init()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);

        if (pausePanel.activeInHierarchy)
        {
            // pause game and music.
            Time.timeScale = 0;
            gamePaused = true;
            Conductor.instance.GetMusicSource().Pause();
        }
        else
        {
            Time.timeScale = 1;
            gamePaused = false;
            Conductor.instance.GetMusicSource().Play();
            Conductor.instance.dspTimeOffset = (float)AudioSettings.dspTime - Conductor.instance.dspSongTime - Conductor.instance.songPosition;
        }
    }
}

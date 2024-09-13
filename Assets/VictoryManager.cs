using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        victoryPanel.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        victoryPanel.SetActive(false);
        Application.LoadLevel("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}

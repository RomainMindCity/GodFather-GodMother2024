using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            victoryPanel.SetActive(true);
            victoryPanel.GetComponent<Canvas>().sortingOrder = 1;
        }
    }

    public void Restart()
    {
        victoryPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerController._end = false;
        HeartbeatUI.end = false;
        Panic.end = false;
        victoryPanel.GetComponent<Canvas>().sortingOrder = 0;
    }

    public void MainMenu()
    {
        victoryPanel.SetActive(false);
        SceneManager.LoadScene("LoadingScene");
        PlayerController._end = false;
        HeartbeatUI.end = false;
        Panic.end = false;
        victoryPanel.GetComponent<Canvas>().sortingOrder = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }
}

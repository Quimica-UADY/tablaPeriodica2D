using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public void navigate(string level)
    {

        Debug.Log(PlayerPrefs.GetString("Token"));
        switch (level)
        {
            case "play":
                SceneManager.LoadScene("LevelMenu");
                break;
            case "perodicTable":
                SceneManager.LoadScene("PerodicTable");
                break;
            case "profile":
                SceneManager.LoadScene("Profile");
                break;
            case "leaderboard":
                SceneManager.LoadScene("Leaderboard");
                break;
            case "settings":
            SceneManager.LoadScene("Leaderboard");
            break;
        }
        
    }
}
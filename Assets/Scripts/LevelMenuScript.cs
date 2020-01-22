using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuScript : MonoBehaviour
{
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;

    // Start is called before the first frame update
    void Start()
    {
        level2.SetActive(false);
        level3.SetActive(false);
        level4.SetActive(false);
        level5.SetActive(false);
        level6.SetActive(false);
    }

    public void navigate(string level){
        Debug.Log(PlayerPrefs.GetString("Token"));
        switch (level)
        {
            case "level1":
                SceneManager.LoadScene("Level1");
                break;
            case "level2":
                SceneManager.LoadScene("Level1");
                break;
            case "level3":
                SceneManager.LoadScene("Level1");
                break;
            case "level4":
                SceneManager.LoadScene("Level1");
                break;
            case "level5":
                SceneManager.LoadScene("Level1");
                break;
            case "level6":
                SceneManager.LoadScene("Level1");
                break;
        }
    }
}

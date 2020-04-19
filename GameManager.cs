using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager Instance;


    /*void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }*/

    public void Play()
    {
        
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    public void Intro()
    {
        SceneManager.LoadScene("Intro");
    }
}

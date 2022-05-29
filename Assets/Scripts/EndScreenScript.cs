using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //check who won and enable texts etc etc
    }


    public void clickMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void clickNextLevel()
    {
        int num = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(num + 1);
    }
}

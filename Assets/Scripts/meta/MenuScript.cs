using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject Logo, StartButton, CreditsButton, BackButton, CreditsText, Instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStart()
    {
        //SceneManager.LoadScene("DaniScene");
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        Logo.SetActive(false);
        Instructions.SetActive(true);

    }
    public void ClickContinue()
    {
        SceneManager.LoadScene("DaniScene");

    }
    public void ClickCredits()
    {
        StartButton.SetActive(false);
        CreditsButton.SetActive(false);
        Logo.SetActive(false);
        BackButton.SetActive(true);
        CreditsText.SetActive(true);
    }

    public void ClickBack()
    {
        StartButton.SetActive(true);
        CreditsButton.SetActive(true);
        Logo.SetActive(true);
        BackButton.SetActive(false);
        CreditsText.SetActive(false);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePreloader : MonoBehaviour
{
    [SerializeField] private bool debugMode = false;

    CanvasGroup fade;
    float loadTime;
    float minLoadTime = 2.0f;

    private void Start()
    {
        if (debugMode)
            SceneManager.LoadScene("MainMenu");

        fade = FindObjectOfType<CanvasGroup>();
        fade.alpha = 1.0f;


        //We can do all kinds of loading at this point

        if (Time.time < minLoadTime)
            loadTime = minLoadTime;
        else
            loadTime = Time.time;
    }

    private void Update()
    {
        if(Time.time< minLoadTime)
        {
            fade.alpha = 1 - Time.time;
        }

        if(Time.time >=minLoadTime && loadTime != 0)
        {
            fade.alpha = Time.time - minLoadTime;
            if (fade.alpha >= 1)
            {
                var newSceneIndex = SaveManager.instance.state.levelsCompleted + 2;

                if (newSceneIndex >= SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else
                {
                    SceneManager.LoadScene(newSceneIndex);
                    AudioManager.instance.Play("menu");
                }
            }
        }
    }

}

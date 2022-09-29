using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    void Start()
    {        
        StartCoroutine(nameof(StartLoading));
    }
    IEnumerator StartLoading()
    {
        int level = RandomLevel();
        AsyncOperation async = SceneManager.LoadSceneAsync(level); // Random level açar
        while (!async.isDone)
        {
            progressBar.value = async.progress; // Loading ekranı ilerler
            yield return null;
        }
    }
    private int RandomLevel()
    {
        // Random level dönderir
        int randomlevel = Random.Range(2, SceneManager.sceneCountInBuildSettings);  
        return randomlevel;
    }
}

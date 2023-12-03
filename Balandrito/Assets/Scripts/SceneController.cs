using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para poder realizar carga de escenas
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //public VideoPlayer video;
    //public RawImage raw;

    private void Start()
    {
        //raw.enabled = false;
        //video.Play();
    }

    /// <summary>
    /// Carga la escena cuyo nombre se ha especificado como parámetro
    /// </summary>
    /// <param name="nextScene"></param>
    public void ChangeScene(string nextScene)
    {
        // Nos aseguramos de que no se realicen cambios de escena con el tiempo parado
        Time.timeScale = 1;

        // Cambiamos a la escena especificada
        SceneManager.LoadScene(nextScene);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        ChangeScene("1_Innocence");
    }

    /// <summary>
    /// Carga la escena cuyo nombre se ha especificado como parámetro
    /// </summary>
    public void GoToGame()
    {
        // Nos aseguramos de que no se realicen cambios de escena con el tiempo parado
        Time.timeScale = 1;

        //raw.enabled = false;
        //video.Pause();

        // Cambiamos a la escena especificada
        SceneManager.LoadScene("1_Innocence");
    }

    public void PlayVideo()
    {
        //raw.enabled = true;
        //video.Play();

        Invoke("GoToGame", 11);
    }
}

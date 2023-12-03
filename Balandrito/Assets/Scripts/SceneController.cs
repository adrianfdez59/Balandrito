using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para poder realizar carga de escenas
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
}

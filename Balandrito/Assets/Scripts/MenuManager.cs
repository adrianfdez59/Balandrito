using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{

    /// <summary>
    /// Cierra el juego (solo funcionar� en la build)
    /// </summary>
    public void QuitGame()
    {
#if UNITY_STANDALONE
        // Cierra el juego en la build
        Application.Quit();
#endif

#if UNITY_EDITOR
        // Desactiva la ejecuci�n del proyecto en Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

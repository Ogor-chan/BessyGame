using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject authorsPanel;
    [SerializeField] GameObject playPanel;

    [Header("Scene")]
    [SerializeField] private UnityEngine.Object entrySceneName;
    [SerializeField] private UnityEngine.Object crystalLakeSceneName;


    private void Awake()
    {
        DisablePanels();
    }


    private void DisablePanels()
    {
        quitPanel.SetActive(false);
        settingsPanel.SetActive(false);
        authorsPanel.SetActive(false);
        playPanel.SetActive(false);

    }

    //skrypt ten w przysz³oœci siê rozwinie jak powstanie system checkpointów oraz zapisywania danych. na razie przenosi nas do gry
    public void PlayGame()
    {
        Debug.Log("Loaded scene: " + entrySceneName.name);
        SceneManager.LoadScene(entrySceneName.name);
    }


    public void QuitApplication()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

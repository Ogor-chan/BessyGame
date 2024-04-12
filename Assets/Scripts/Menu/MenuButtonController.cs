using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject authorsPanel;
    [SerializeField] GameObject playPanel;
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


    public void QuitApplication()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

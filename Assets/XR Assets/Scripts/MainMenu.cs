using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SnapTurnProviderBase turnProvider;
    [SerializeField]
    private LocomotionController locomotion;

    private void OnEnable()
    {
        turnProvider.enabled = false;
        locomotion.enableLocomotion = false;
    }

    private void OnDisable()
    {
        turnProvider.enabled = true;
        locomotion.enableLocomotion = true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

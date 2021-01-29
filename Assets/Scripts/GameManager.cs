using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum GameState { Opening, Game, Menu }

public class GameManager : MonoBehaviour
{
    //scene manager
    private SceneManager sceneManager;

    //input manager
    private PlayerInput input;

    //singleton instance
    private static GameManager _s;

    [SerializeField]
    private UnityEvent OnPause;

    [SerializeField]
    private UnityEvent OnUnpause;

    //==> I GOT TO CONTROL MUSIC AS SCREEN CHANGES !!!REMEMBER THIS!!!
    [SerializeField]
    private AudioListener audioListener;

    void Awake() {
        _s = this;
        input = new PlayerInput();
    }

    private void OnEnable() {
        
    }

    private void OnDisable() {
        
    }

    void Update() {
        
    }

    public static void Pause() {
        _s.OnPause.Invoke();
        Time.timeScale = 0f;
    }

    public static void Unpause() {
        _s.OnUnpause.Invoke();
        Time.timeScale = 1f;
    }
}

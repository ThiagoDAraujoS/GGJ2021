using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState { Opening, Game, Menu }

public struct DebugVariables
{
	// Debug Override
	private const bool _debugEnabled = true;

	// Debug Variables
	private const bool _dropAreaEditorVisualsEnabled = true;

	public bool DropAreaEditorVisualsEnabled { get { return _debugEnabled && _dropAreaEditorVisualsEnabled; } }
}

public class GameManager : MonoBehaviour
{
	// Debug variables.
	public static DebugVariables DebugVariables { get; } = new DebugVariables();

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

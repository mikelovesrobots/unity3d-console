using UnityEngine;
using System.Collections;

public class ConsoleGUI : MonoBehaviour {
    public ConsoleAction escapeAction;
    public ConsoleAction submitAction;
    [HideInInspector]
        public string input = "";
    private ConsoleLog consoleLog;
    private Rect consoleRect;
    private bool focus = false;
    private const int WINDOW_ID = 50;

    private void Start() {
        consoleRect = new Rect(0, 0, Screen.width, Mathf.Min(300, Screen.height));
        consoleLog = ConsoleLog.Instance;
    }

    private void OnEnable() {
        focus = true;
    }

    private void OnDisable() {
        focus = true;
    }

    public void OnGUI() {
        GUILayout.Window(WINDOW_ID, consoleRect, RenderWindow, "Console");
    }

    private void RenderWindow(int id) {
        HandleSubmit();
        HandleEscape();

        GUILayout.BeginScrollView(Vector2.zero);
        GUILayout.Label(consoleLog.log);
        GUILayout.EndScrollView();
        GUI.SetNextControlName("input");
        input = GUILayout.TextField(input);
        if (focus) {
            GUI.FocusControl("input");
            focus = false;
        }
    }

    private void HandleSubmit() {
        if (KeyDown("[enter]") || KeyDown("return")) {
            if (submitAction != null) {
                submitAction.Activate();
            }
            input = "";
        }
    }

    private void HandleEscape() {
        if (KeyDown("escape") || KeyDown("`")) {
            escapeAction.Activate();
            input = "";
        }
    }

    private bool KeyDown(string key) {
        return Event.current.Equals(Event.KeyboardEvent(key));
    }
}

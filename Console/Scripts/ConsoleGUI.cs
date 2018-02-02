using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsoleGUI : MonoBehaviour {
    public ConsoleAction escapeAction;
    public ConsoleAction submitAction;
    [HideInInspector]
    public string input = "";
    private ConsoleLog consoleLog;
    private Rect consoleRect;
    private bool focus = false;
    private const int WINDOW_ID = 50;
    private const int MIN_CONSOLE_HEIGHT = 300;

    private ConsoleCommandsRepository consoleCommandsRepository;

    private int maxConsoleHistorySize = 100;
    private int consoleHistoryPosition = 0;
    private List<string> consoleHistoryCommands = new List<string>();
    private bool fixPositionNextFrame = false; // a hack because the up arrow moves the cursor to the first position.

    private float scrollPosition;


    private void Start() {
        consoleRect = new Rect(0, 0, Screen.width, Mathf.Min(MIN_CONSOLE_HEIGHT, Screen.height));
        consoleLog = ConsoleLog.Instance;
        consoleCommandsRepository = ConsoleCommandsRepository.Instance;
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
        if (fixPositionNextFrame)
        {
            MoveCursorToPos(input.Length);
            fixPositionNextFrame = false;
        }
        HandleSubmit();
        HandleEscape();
        HandleTab();
        HandleUp();
        HandleDown();
        scrollPosition = GUILayout.BeginScrollView(new Vector2(0, scrollPosition), false, true).y;
        if (consoleLog.fresh)
        {
            scrollPosition = consoleLog.scrollLength;
            consoleLog.fresh = false;
        }
        GUILayout.Label(consoleLog.log);
        GUILayout.EndScrollView();
        GUI.SetNextControlName("input");
        input = GUILayout.TextField(input);
        if (focus) {
            GUI.FocusControl("input");
            focus = false;
        }
    }


    private string LargestSubString(string in1, string in2) // takes two strings and returns the largest matching substring.
    {
        string output = "";
        int smallestLen = Mathf.Min(in1.Length, in2.Length);
        for (int i = 0; i<smallestLen; i++) {
            if (in1[i] == in2[i]) output += in1[i];
            else return output;
        }
        return output;
    }

    private void MoveCursorToPos(int position)
    {
        TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
#if UNITY_5_2 || UNITY_5_3 || UNITY_5_3_OR_NEWER
        editor.selectIndex = position;
        editor.cursorIndex = position;
#else
        editor.selectPos = position;
        editor.pos = position;
#endif
        return;
    }

    private void HandleTab()
    {
        if (!KeyDown("tab")) 
            return;

        if (input != "") { // don't do anything if the input field is still blank.
            List<string> search = consoleCommandsRepository.SearchCommands(input);
            if (search.Count == 0) { // nothing found
                consoleLog.Log("No commands start with \"" + input + "\".");
                input = ""; // clear input
            } else if (search.Count == 1) {
                input = search[0] + " "; // only found one command - type it in for the guy
                MoveCursorToPos(input.Length);
            } else {
                consoleLog.Log("Commands starting with \"" + input + "\":");
                string largestMatch = search[0]; // keep track of the largest substring that matches all searches
                foreach (string command in search)
                {
                    consoleLog.Log(command);
                    largestMatch = LargestSubString(largestMatch, command);
                }
                input = largestMatch;
                MoveCursorToPos(input.Length);
            }
        }
    }

    private void HandleUp()
    {
        if (!KeyDown("up"))
            return;

        consoleHistoryPosition += 1;
        if (consoleHistoryPosition > consoleHistoryCommands.Count - 1) consoleHistoryPosition = consoleHistoryCommands.Count - 1;
        input = consoleHistoryCommands[consoleHistoryPosition];
        fixPositionNextFrame = true;
    }

    private void HandleDown()
    {
        if (!KeyDown("down"))
            return;

        consoleHistoryPosition -= 1;
        if (consoleHistoryPosition < 0) {
            consoleHistoryPosition = -1;
            input = "";
        } else {
            input = consoleHistoryCommands[consoleHistoryPosition];
        }
        MoveCursorToPos(input.Length);
    }

    private void HandleSubmit() {
        if (KeyDown("[enter]") || KeyDown("return")) {
            consoleHistoryPosition = -1; // up arrow or down arrow will set it to 0, which is the last command typed.
            if (submitAction != null) {
                submitAction.Activate();
                consoleHistoryCommands.Insert(0, input);
                if (consoleHistoryCommands.Count > maxConsoleHistorySize)
                    consoleHistoryCommands.RemoveAt(consoleHistoryCommands.Count-1);
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

    private void Update() {
        if (input == "`")
            input = "";
    }

    private bool KeyDown(string key) {
        return Event.current.Equals(Event.KeyboardEvent(key));
    }
}

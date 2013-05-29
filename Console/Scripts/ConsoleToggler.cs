using UnityEngine;
using System.Collections;

public class ConsoleToggler : MonoBehaviour {
    private bool consoleEnabled = false;
    public ConsoleAction ConsoleOpenAction;
    public ConsoleAction ConsoleCloseAction;

    void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            ToggleConsole();
        }
    }

    private void ToggleConsole() {
        consoleEnabled = !consoleEnabled;
        if (consoleEnabled) {
            ConsoleOpenAction.Activate();
        } else {
            ConsoleCloseAction.Activate();
        }
    }
}

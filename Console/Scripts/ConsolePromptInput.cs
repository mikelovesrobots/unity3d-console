using UnityEngine;
using System.Collections;

public class ConsolePromptInput : ConsoleInput {
	public ConsoleGUI consoleGUI;

	public override void Interpret(ref string response) {
		response = consoleGUI.input;
		ConsoleLog.Instance.Log (response);
	}
}

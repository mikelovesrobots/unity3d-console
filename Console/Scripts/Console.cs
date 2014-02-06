using UnityEngine;
using System.Collections;

public class Console {

	private static Console instance;
	public static Console Instance {
		get {
			if(instance == null)
				instance = new Console();
			return instance;
		}
	}

	private ConsoleLog consoleLog;

	public bool inPromptMode = false;
	public string promptResponse = "";

	public delegate void OnConsoleSubmit(string response);
	public OnConsoleSubmit onConsoleSubmit;


	/// <summary>
	/// Prompt's the user for input info. User response can be used in the callback function.
	/// </summary>
	/// <param name="prompt">Text that will be displayed to the user, ie. "Enter Password: "</param>
	/// <param name="callback">Callback function to execute once user has submitted response. </param>
	/// Note that callback has a function signature of void callback(string)
	public void Prompt(string prompt, OnConsoleSubmit callback) {
		onConsoleSubmit = callback;
		ConsoleLog.Instance.Log("> " + prompt);
		inPromptMode = true;
	}
}

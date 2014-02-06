using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class ConsoleSubmitAction : ConsoleAction {

	private Console console;
  	private ConsoleCommandsRepository consoleCommandsRepository;
  	private ConsoleLog consoleLog;

  	public ConsoleGUI consoleGUI;
	public ConsoleInput commandInput;
	public ConsoleInput promptInput;

  private void Start() {
		console = Console.Instance;
    	consoleCommandsRepository = ConsoleCommandsRepository.Instance;
    	consoleLog = ConsoleLog.Instance;
  }

  public override void Activate() {
		string response = "";

		if(console.inPromptMode)
		{
			promptInput.Interpret(ref response);
			console.onConsoleSubmit(response);
			console.inPromptMode = false;
		}
		else
		{
			commandInput.Interpret(ref response);
		}

  }
}

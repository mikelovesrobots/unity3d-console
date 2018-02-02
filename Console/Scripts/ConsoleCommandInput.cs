using UnityEngine;
using System.Collections;
using System.Linq;

public class ConsoleCommandInput : ConsoleInput {
	public ConsoleGUI consoleGUI;
  	private ConsoleCommandsRepository consoleCommandsRepository;
  	private ConsoleLog consoleLog;
	
	private void Start() {
    	consoleCommandsRepository = ConsoleCommandsRepository.Instance;
    	consoleLog = ConsoleLog.Instance;
  	}

	public override void Interpret(ref string response) {
		string[] parts = consoleGUI.input.Split(' ');
		string command = parts[0];
		string[] args = parts.Skip(1).ToArray();
		
		consoleLog.Log("> " + consoleGUI.input);
		if (consoleCommandsRepository.HasCommand(command)) {
			response = consoleCommandsRepository.ExecuteCommand(command, args);
			consoleLog.Log(response);
		} else {
			response = "";
			consoleLog.Log("Command " + command + " not found");
		}
	}
}

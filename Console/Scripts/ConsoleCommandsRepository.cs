using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate string ConsoleCommandCallback(params string[] args);

public class ConsoleCommandsRepository {
  private static ConsoleCommandsRepository instance;
  private Dictionary<string, ConsoleCommandCallback> repository;

  public static ConsoleCommandsRepository Instance {
    get {
      if (instance == null) {
        instance = new ConsoleCommandsRepository();
      }
      return instance;
    }
  }
  public ConsoleCommandsRepository() {
    repository = new Dictionary<string, ConsoleCommandCallback>();
    repository.Add ("listcommands", ListCommands);
    ConsoleLog.Instance.Log ("Type 'listcommands' to get a list of console commands available");
  }

  public string ListCommands(params string[] args) {
    var cmdList = string.Empty;
    if (repository != null)
    {
      foreach (var kvp in repository)
      {
        if (kvp.Key.ToString() != "listcommands")
        {
          cmdList += kvp.Key.ToString() + System.Environment.NewLine;
        }
      }
      return cmdList;
    }
    else
    {
      return "No commands have been setup. Use the RegisterCommand method on your console repository to add new commands.";
    }
  }

  public void RegisterCommand(string command, ConsoleCommandCallback callback) {
    if (command == "listcommands")
    {
      throw new UnityException("You cannot register 'listcommands' as a command, as this is reserved for system use.");
    }
    else 
    {
      repository[command] = new ConsoleCommandCallback(callback);
    }
  }

  public bool HasCommand(string command) {
    return repository.ContainsKey(command);
  }

  public string ExecuteCommand(string command, string[] args) {
    if (HasCommand(command)) {
      return repository[command](args);
    } else {
      return "Command not found";
    } 
  }
}

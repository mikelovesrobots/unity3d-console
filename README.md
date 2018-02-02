In-game Console
=======

Synopsis
--------

Quake-style console plugin for Unity3d.  Toggle the console by pressing tilde (~). Press tab to autocomplete commands.

Screenshot
-----------
![Screenshot](https://dl.dropboxusercontent.com/s/z0gw0h267h0fzz4/Screen%20Shot%202014-01-06%20at%2011.26.19%20AM.png)

Installation
------------

1. Copy the Console directory to your Assets/Plugins folder.  (Make the plugins folder if it doesn't exist.)
2. Drag the console prefab into your scene.
3. Run your scene and press ~ to launch the console.
4. Now register some custom commands

Registering custom commands
---------------
```
using UnityEngine;
using System.Collections;

public class ConsoleCommandRouter : MonoBehaviour {
    void Start() {
        var repo = ConsoleCommandsRepository.Instance;
        repo.RegisterCommand("hi", Hi);
        repo.RegisterCommand("save", Save);
        repo.RegisterCommand("load", Load);
    }

    public string Hi(params string[] args) {
        return "Hey there yourself!";
    }

    public string Save(params string[] args) {
        var filename = args[0];

        //
        // [insert code here saving the game to that filename]
        //

        return "Saved to " + filename;
    }

    public string Load(params string[] args) {
        var filename = args[0];

        //
        // [insert code here loading the game from that filename]
        //

        return "Loaded " + filename;
    }
}
```

The string returned from the console command will be displayed to the in-game 
console log.  Insert newlines in your response to have multiple lines be written
to the log.

Prompting the user for input and using the response
----------------------------------------------------
If you want to prompt the user for input, say "Enter Password: " and do something with the response,
use: 
```
Console.Instance.Prompt(string prompt, void callback(string response));
```

For example:
```
void Start() {
    Console.Instance.Prompt("Enter Password: ", Print);
}

void Print(string response) {
    // Do stuff with response
    Debug.Log(response);
}
```

Logging
-------

```
var logger = ConsoleLog.Instance;
logger.Log("Player died")
```

Logs to the in-game console.

Note from the author
--------------------

Feel free to contribute your changes back!  I love pull requests.

Cheers,

Mike

mikelovesrobots@gmail.com

@mikelovesrobots on Twitter

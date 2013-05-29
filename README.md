In-game Console
=======

Synopsis
--------

Quake-style console plugin for Unity3d.  Toggle the console by pressing tilde (~).

Screenshot
-----------
* https://dl.dropboxusercontent.com/u/664907/Screenshots/o2i4.png

Installation
------------

Copy the Console directory to your Assets/Plugins folder.  
Make the plugins folder if it doesn't exist.

Registering custom commands
---------------
```
using UnityEngine;
using System.Collections;

public class ConsoleCommandRouter : MonoBehaviour {
    void Start () {
        var repo = ConsoleCommandsRepository.Instance;
        repo.RegisterCommand("save", Save);
        repo.RegisterCommand("load", Load);
    }

    public string Save(params string[] args) {
        var filename = args[0];
        new LevelSaver().Save(filename);
        return "Saved to " + filename;
    }

    public string Load(params string[] args) {
        var filename = args[0];
        new LevelLoader().Load(filename);
        return "Loaded " + filename;
    }
}
```

The string returned from the console command will be written to the in-game 
console log.

Logging
-------

```
var logger = ConsoleLog.Instance;
logger.Log("Player died")
```

Logs to the in-game console.

Note from the author
--------------------

Contact me if you've got any questions, bugs or contract work to throw my way.

Cheers,

Mike

mikelovesrobots@gmail.com

@mikelovesrobots on Twitter

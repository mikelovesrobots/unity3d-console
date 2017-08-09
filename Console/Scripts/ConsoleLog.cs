using UnityEngine;
using System.Collections;

public class ConsoleLog {
    private static ConsoleLog instance;
    public static ConsoleLog Instance {
        get {
            if (instance == null) {
                instance = new ConsoleLog();
            }
            return instance;
        }
    }

    public string log = "";
	public int scrollLength;
	public bool fresh = false;

    public void Log(string message) {
        log += message + "\n";
		fresh = true;
		Debug.Log((message + "\n").Split('\n').Length);
		scrollLength += ((message+"\n").Split('\n').Length)*20;
    }
}

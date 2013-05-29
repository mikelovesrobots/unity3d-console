using UnityEngine;
using System.Collections;

public class ConsoleCloseAction : ConsoleAction {
    public GameObject ConsoleGui;

    public override void Activate() {
        ConsoleGui.SetActive(false);
    }
}

using UnityEngine;
using System.Collections;

public class ConsoleOpenAction : ConsoleAction {
    public GameObject ConsoleGui;

    public override void Activate() {
        ConsoleGui.SetActive(true);
    }
}

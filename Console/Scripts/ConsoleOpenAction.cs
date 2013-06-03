using UnityEngine;
using System.Collections;

public class ConsoleOpenAction : ConsoleAction {
    public GameObject ConsoleGui;

    public override void Activate() {
#if UNITY_3_5
        ConsoleGui.active = false;
#else
        ConsoleGui.SetActive(true);
#endif
    }
}

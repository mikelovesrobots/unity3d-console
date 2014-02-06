using UnityEngine;
using System.Collections;

public abstract class ConsoleInput : MonoBehaviour {
  public abstract void Interpret(ref string response);
}

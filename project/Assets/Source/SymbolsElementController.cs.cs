using System; 

using UnityEngine;
using UnityEngine.UI;

using static GameController;

public class SymbolsElementController : MonoBehaviour {
  GameController gameController;
  Text text;

  void Awake () {
    gameController = UnityEngine.Object.FindObjectOfType<GameController> ();
    text = GetComponent<Text> ();
  }

  void Update () {
    var symbols = gameController.symbols;
    text.text = symbols.Length > 0 ? String.Join(",", symbols) : "empty";
  }
}

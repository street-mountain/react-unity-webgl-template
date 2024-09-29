using System.Runtime.InteropServices;
using UnityEngine;
using System;

// public class JSONEventArgs
// {
//     public JSONEventArgs(string text) { Text = text; }
//     public string Text { get; } // readonly
// }

public class GameController : MonoBehaviour {
  public bool isPlaying { private set; get; } = false;
  public float time { private set; get; }
  public int score;

  [DllImport ("__Internal")]
  static extern void GameOver (float time, int score);


  void Awake () {
    time = 0;
  }

  void Update () {
    if (isPlaying == false) {
      return;
    }
    time -= Time.deltaTime;
    if (time <= 0 || score >= 25) {
      isPlaying = false;
#if UNITY_WEBGL && !UNITY_EDITOR
      GameOver (time, score);
#endif
    }
  }

  public void StartGame (int time) {
    this.time = time;
    score = 0;
    isPlaying = true;
  }


  // From Tonatomy 
      // This will hold the list of symbols received from JavaScript
    public string[] symbols;

    // Define a class to match the JSON structure for symbols
    [System.Serializable]
    public class SymbolsData
    {
        public string[] symbols;
    }

    // Method to be called from JavaScript to update wallet symbols
    public void OnNewWalletSymbols(string jsonArray)
    {
      
        // Deserialize the JSON array into a string array
        SymbolsData data = JsonUtility.FromJson<SymbolsData>(jsonArray);
        symbols = data.symbols;

        // Process the symbols as needed
        foreach (var symbol in symbols)
        {
            Debug.Log("Received symbol: " + symbol);
        }

        // Additional processing if needed
    }

//     public delegate void JSONRequestHandler(object sender, JSONEventArgs e);

// public event JSONRequestHandler JSONRequestEvent;

  public class JSONRequest {
    public string reqId;
    public string[] request;
  }
    public void OnJSONRequest(string jsonString)
    {
                Debug.Log("json request handler");
                Debug.Log(jsonString);

        // Deserialize the JSON array into a string array
        JSONRequest data = JsonUtility.FromJson<JSONRequest>(jsonString);
                        Debug.Log(data.reqId);
                        Debug.Log(data.request);
                                                Debug.Log(data);


        if (Array.Exists(data.request, s => s == "Kombis")) {
          Debug.Log("The zombies are coming");
          symbols = new string[] {"Zombies!!"};
        }

        // Process the symbols as needed
        foreach (var symbol in symbols)
        {
            Debug.Log("Received symbol: " + symbol);
        }

        // Additional processing if needed
    }

    // Define a class to match the JSON structure for token balance
    [System.Serializable]
    public class TokenBalance
    {
        public string tokenSymbol;
        public float balance;
    }

    // Import JavaScript function to request token balance update
    [DllImport("__Internal")]
    static extern void RequestTokenBalanceUpdate(string tokenSymbol);

    // Method to call JavaScript to request token balance update
    public void UpdateTokenBalance(string tokenSymbol)
    {
        // Call the JavaScript function
        RequestTokenBalanceUpdate(tokenSymbol);
    }

    // Method to receive token balance data from JavaScript
    public void OnReceiveTokenBalance(string json)
    {
        // Deserialize the JSON string into a TokenBalance object
        TokenBalance tokenBalance = JsonUtility.FromJson<TokenBalance>(json);

        // Process the token balance data
        Debug.Log("Token Symbol: " + tokenBalance.tokenSymbol);
        Debug.Log("Balance: " + tokenBalance.balance);

        // Additional processing if needed
    }
}

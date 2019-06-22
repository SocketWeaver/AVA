using UnityEngine.UI;
using UnityEngine;

public class GUIPlayerScoreRow : MonoBehaviour
{
    public Text NameText;
    public Text KillText;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Display(string playerId, int kill)
    {
        NameText.text = playerId;
        KillText.text = kill.ToString();
    }
}

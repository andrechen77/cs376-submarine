using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public TextMeshProUGUI centerPrompt;
    public TextMeshProUGUI cornerPrompt;

    public void Win() {
        centerPrompt.text = "You win!";
    }

    public void Lose() {
        centerPrompt.text = "You lose!";
    }

    public void IndicateFuel(float amt) {
        cornerPrompt.text = $"Fuel: {amt}";
    }
}

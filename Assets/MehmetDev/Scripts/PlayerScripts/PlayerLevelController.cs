using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerLevelController : MonoBehaviour
{
    public int playerLevel = 1;
    public TextMeshProUGUI playerLevelText;
    public Image deathImage;

    void Start()
    {
        UpdateLevelText();
        deathImage.enabled = false;
    }
    public void IncreaseLevel(int amount)
    {
        playerLevel += amount;
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        playerLevelText.text = "Lv. " + playerLevel.ToString();
    }

    public int GetCurrentLevel()
    {
        return playerLevel;
    }

    public void GetDeathImage()
    {
        deathImage.enabled = true;
    }
}

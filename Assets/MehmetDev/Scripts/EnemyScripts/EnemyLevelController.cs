using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EnemyLevelController : MonoBehaviour
{
    public int enemyLevel = 1;
    public TextMeshProUGUI enemyLevelText;
    public Image enemyDeathImage;
    void Start()
    {
        RandomLevel();
        enemyDeathImage.enabled = false;
    }
    public int RandomLevel()
    {

        enemyLevel = Random.Range(2, 6);
        enemyLevelText.text = "Lv. " + enemyLevel.ToString();
        return enemyLevel;
    }

    public int GetEnemyLevel()
    {
        return enemyLevel;
    }
    public void GetDeathImage()
    {
        enemyDeathImage.enabled = true;
    }
}

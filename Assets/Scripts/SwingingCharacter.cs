using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwingingCharacter : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text levelNumberText;

    public LevelInfo levelInfo;

    private float counter = 0;

    void Start()
    {
        levelText.SetText(LevelInfo.getDescription(levelInfo.curLevel()));
        levelNumberText.SetText("Level: " + (levelInfo.curLevel() + 1));
        counter = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > 0) {
            counter -= 1 * Time.deltaTime;
            if(counter <= 0) {
                levelText.SetText("");
            }
        }
    }

    public void hasWon()
    {
        levelInfo.loadNextLevel();
    }
}
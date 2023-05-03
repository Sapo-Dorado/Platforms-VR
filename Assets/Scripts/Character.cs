using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Character : MonoBehaviour
{
    public TeleportationProvider provider = null;
    public TMP_Text levelText;
    public TMP_Text levelNumberText;
    public int levelIdx;

    private float counter = 0;

    void Start()
    {
        levelText.SetText(LevelInfo.getDescription(levelIdx));
        levelNumberText.SetText("Level: " + (levelIdx + 1));
        counter = 5;
    }

    // Update is called once per frame
    void Update()
    {
        updateCounter();

        if(transform.position.y < -25) {
            TeleportRequest request = CreateTeleportationRequest(new Vector3(0,0,0));
            provider.QueueTeleportRequest(request);
        }
    }

    void updateCounter() {
        if(counter > 0) {
            counter -= 1 * Time.deltaTime;
            if(counter <= 0) {
                levelText.SetText("");
            }
        }
    }

    private TeleportRequest CreateTeleportationRequest(Vector3 pos) {
        return new TeleportRequest()
        {
            destinationPosition = pos
        };

    }

    public void hasWon()
    {
        levelIdx = LevelInfo.nextLevel(levelIdx);
        SceneManager.LoadScene(LevelInfo.getLevel(levelIdx));
    }
}

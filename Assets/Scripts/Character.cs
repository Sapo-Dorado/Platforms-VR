using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Character : MonoBehaviour
{
    public TeleportationProvider provider = null;
    public Jetpack jetpack;
    private Rigidbody body;
    public TMP_Text levelText;
    public TMP_Text levelNumberText;

    public LevelInfo levelInfo;

    private float counter = 0;
    private bool nextLevel = false;

    void Start()
    {
        body = transform.GetComponentInParent<Rigidbody>();
        levelText.SetText(LevelInfo.getDescription(levelInfo.curLevel()));
        levelNumberText.SetText("  Level: " + (levelInfo.curLevel() + 1));
        counter = 5;
    }

    // Update is called once per frame
    void Update()
    {
        updateCounter();

        if(Vector3.Magnitude(transform.position) > 100) {
            levelText.SetText("You got too far away. Try again");
            counter = 10;
            TeleportRequest request = CreateTeleportationRequest(new Vector3(0,0,0));
            provider.QueueTeleportRequest(request);
            jetpack.resetMomentum();
        }
    }

    void updateCounter() {
        if(counter > 0) {
            counter -= 1 * Time.deltaTime;
            if(counter <= 0) {
                levelText.SetText("");
                if(nextLevel) {
                    levelInfo.loadNextLevel();
                }
            }
        }
    }

    public void addGravity() {
        body.useGravity = true;
        nextLevel = true;
        counter = 3;
    }

    private TeleportRequest CreateTeleportationRequest(Vector3 pos) {
        return new TeleportRequest()
        {
            destinationPosition = pos
        };

    }

    public void hasWon()
    {
        levelInfo.loadNextLevel();
    }
}

using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
  public int curLevelIdx;
  private static string[] levels = new string[] {"0G-Level-1", "0G-Level-2", "0G-Level-3", "0G-Level-4", "0G-Level-5", "Swigning-Level-1", "Swinging-Level-2", "End"};
  private static string[] descriptions = new string[] {
    "<b>Level One</b>\nUse your jetpacks to make it to the green zone and escape.",
    "<b>Level Two</b>\nSince you are in a vacuum, you don't lose momentum. Thus you can save a lot of fuel.",
    "<b>Level Three</b>\nNo fuel :( \nWhat can you do? (hint: look behind you)",
    "<B>Level Four</b>\nHow can you get out of this tube?\n(hint: look to the side)",
    "<B>Level Five</b>\nPush the button to activate gravity.",
    "<B>Level Six</b>\nPoint at a platform and press trigger to connect a rope and swing to the next platform.",
    "<B>Level Seven</b>\nMoving platforms will carry your rope with it.",
    "<B>End</b>\nYou Win! Enter the green box to play again."
  };
  private static int levelCount = 8;

  public static string getLevel(int idx) {
    return levels[idx];
  }

  public static string getDescription(int idx) {
    return "<align=\"center\">" + descriptions[idx];
  }

  public static int nextLevel(int idx) {
    Debug.Log((idx + 1) % levelCount);
    return (idx + 1) % levelCount;
  }

  public void loadNextLevel() {
    curLevelIdx = nextLevel(curLevelIdx);
    SceneManager.LoadScene(getLevel(curLevelIdx));
  }

  public int curLevel() {
    return curLevelIdx;
  }

}
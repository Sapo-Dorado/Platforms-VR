using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
  public int curLevelIdx;
  private static string[] levels = new string[] {"0G-Level-1", "0G-Level-2", "0G-Level-3", "0G-Level-4", "Swinging-Level-1"};
  private static string[] descriptions = new string[] {
    "<b>Level One</b>\nUse your jetpacks to make it to the green zone and escape.",
    "<b>Level Two</b>\nSince you are in a vacuum, you don't lose momentum. Thus you can save a lot of fuel.",
    "<b>Level Three</b>\nNo fuel :( \nWhat can you do? (hint: look behind you)",
    "<B>Level Four</b>\nHow can you get out of this tube?\n(hint: look to the side)",
    "<B>Level Five</b>"
  };
  private static int levelCount = 5;

  public static string getLevel(int idx) {
    return levels[idx];
  }

  public static string getDescription(int idx) {
    return "<align=\"center\">" + descriptions[idx];
  }

  public static int nextLevel(int idx) {
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
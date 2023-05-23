public class LevelInfo {
  private static string[] levels = new string[] {"0G-Level-1", "0G-Level-2", "0G-Level-3"};
  private static string[] descriptions = new string[] {
    "<b>Level One</b>\nUse your jetpacks to make it to the green zone and escape.",
    "<b>Level Two</b>\nSince you are in a vacuum, you don't lose momentum. Thus you can save a lot of fuel.",
    "<b>Level Three</b>\nNo fuel :( \nWhat can you do? (hint: look behind you)"
  };
  private static int levelCount = 3;

  public static string getLevel(int idx) {
    return levels[idx];
  }

  public static string getDescription(int idx) {
    return "<align=\"center\">" + descriptions[idx];
  }

  public static int nextLevel(int idx) {
    return (idx + 1) % levelCount;
  }
}
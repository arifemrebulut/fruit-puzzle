using UnityEngine;

public enum LevelStats
{
    OnMenu,
    OnPlay,
    OnLevelComplete,
    OnFinishedFruitScene,
}

public class GameManager : MonoBehaviour
{
    public static LevelStats currentLevelStat;

    private void Start()
    {
        currentLevelStat = LevelStats.OnPlay;
        EventBroker.CallOnLevelStart();
    }

    public static void SwitchCurrentLevelStat(LevelStats desiredLevelStat)
    {
        currentLevelStat = desiredLevelStat;
    }
}

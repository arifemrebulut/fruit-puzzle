using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public enum LevelStats
{
    OnPlay,
    OnLevelComplete,
    OnFinishScene
}

public class LevelManager : MonoBehaviour
{
    [Header("LevelCompleteAnimations")]

    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] float greatTextAnimationDuration;
    [SerializeField] List<ParticleSystem> levelCompleteParticleEffects;

    private Vector3 fruitPosition;
    public static LevelStats currentLevelStat;

    private void OnEnable()
    {
        EventBroker.OnLevelComplete += PlayLevelCompleteAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnLevelComplete -= PlayLevelCompleteAnimations;
    }

    private void PlayLevelCompleteAnimations()
    {
        fruitPosition = FindObjectOfType<FruitMovement>().transform.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(greatText.rectTransform.DOScale(new Vector3(2f, 2f, 2f), greatTextAnimationDuration).SetEase(Ease.OutBounce));
        sequence.Append(greatText.rectTransform.DOScale(Vector3.zero, greatTextAnimationDuration).SetEase(Ease.OutBounce).SetDelay(2));

        for (int i = 0; i < levelCompleteParticleEffects.Count; i++)
        {
            ParticleSystem effect = Instantiate(levelCompleteParticleEffects[i], fruitPosition, transform.rotation);
        }

        SwitchCurrentLevelStat(LevelStats.OnFinishScene);
    }

    public static void SwitchCurrentLevelStat(LevelStats desiredLevelStat)
    {
        currentLevelStat = desiredLevelStat;
    }

}

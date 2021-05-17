using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LevelAnimations : MonoBehaviour
{
    #region Animation Fields

    [Header("Fruit Complete Animations")]
    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] float greatTextAnimationDuration;
    [SerializeField] List<ParticleSystem> fruitCompleteParticleEffects;

    [Header("Camera Animations")]
    [SerializeField] Camera camera;
    [SerializeField] float cameraAnimationsDuration;

    [SerializeField] Vector3 cameraStartTargetPosition;
    [SerializeField] Vector3 cameraStartTargetRotation;

    [SerializeField] Vector3 cameraFinishTargetPosition;
    [SerializeField] Vector3 cameraFinishTargetRotation;

    [Header("Level Past Animations")]
    [SerializeField] TextMeshProUGUI levelPassedText;
    [SerializeField] List<ParticleSystem> levelPassedParticleEffects;
    [SerializeField] float levelPassedTextAnimationDuration;

    private Vector3 fruitPosition;
    private Vector3 finishedFruitPosition;

    #endregion  

    #region Subscribing and Unsubscribing to events for Playing Animations

    private void OnEnable()
    {
        EventBroker.OnLevelStart += PlayCameraStartLevelAnimation;
        EventBroker.OnFruitRiseEnd += PlayFruitCompleteAnimations;
        EventBroker.OnLevelPassed += PlayLevelPassedAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnLevelStart -= PlayCameraStartLevelAnimation;
        EventBroker.OnFruitComplete -= PlayFruitCompleteAnimations;
        EventBroker.OnLevelPassed -= PlayLevelPassedAnimations;
    }

    #endregion

    private void PlayCameraStartLevelAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(camera.transform.DOMove(cameraStartTargetPosition, cameraAnimationsDuration).SetEase(Ease.OutQuad));
        sequence.Join(camera.transform.DORotate(cameraStartTargetRotation, cameraAnimationsDuration)).SetDelay(0.5f);
    }

    private void PlayCameraOnFruitCompleteAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(camera.transform.DOMove(cameraFinishTargetPosition, cameraAnimationsDuration).SetEase(Ease.OutQuad)
            .OnStart(EventBroker.CallOnFinishedFruitScene));
        sequence.Join(camera.transform.DORotate(cameraFinishTargetRotation, cameraAnimationsDuration));
    }

    private void PlayFruitCompleteAnimations()
    {
        fruitPosition = FindObjectOfType<FruitMovement>().transform.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(greatText.rectTransform.DOScale(new Vector3(2f, 2f, 2f), greatTextAnimationDuration).SetEase(Ease.OutBounce)
            .OnComplete(PlayCameraOnFruitCompleteAnimation));
        sequence.Append(greatText.rectTransform.DOScale(Vector3.zero, greatTextAnimationDuration / 2).SetEase(Ease.Linear));

        for (int i = 0; i < fruitCompleteParticleEffects.Count; i++)
        {
            ParticleSystem effect = Instantiate(fruitCompleteParticleEffects[i], fruitPosition, Quaternion.identity);
        }

        GameManager.SwitchCurrentLevelStat(LevelStats.OnFinishedFruitScene);
    }
 
    private void PlayLevelPassedAnimations()
    {
        finishedFruitPosition = FindObjectOfType<FinishedFruitAnimations>().transform.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(levelPassedText.rectTransform.DOScale(new Vector3(2f, 2f, 2f), levelPassedTextAnimationDuration).SetEase(Ease.OutQuad));

        for (int i = 0; i < levelPassedParticleEffects.Count; i++)
        {
            ParticleSystem effect = Instantiate(levelPassedParticleEffects[i], finishedFruitPosition, Quaternion.identity);
        }
    }
}

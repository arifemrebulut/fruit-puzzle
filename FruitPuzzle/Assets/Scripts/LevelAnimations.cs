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

    private Vector3 fruitPosition;

    #endregion  

    #region Subscribing and Unsubscribing to events for Playing Animations

    private void OnEnable()
    {
        EventBroker.OnLevelStart += PlayCameraStartLevelAnimation;
        EventBroker.OnFruitComplete += PlayFruitCompleteAnimations;
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

        sequence.Append(camera.transform.DOMove(cameraFinishTargetPosition, cameraAnimationsDuration).SetEase(Ease.OutQuad));
        sequence.Join(camera.transform.DORotate(cameraFinishTargetRotation, cameraAnimationsDuration));
    }

    private void PlayFruitCompleteAnimations()
    {
        fruitPosition = FindObjectOfType<FruitMovement>().transform.position;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(greatText.rectTransform.DOScale(new Vector3(2f, 2f, 2f), greatTextAnimationDuration).SetEase(Ease.OutBounce)
            .OnComplete(PlayCameraOnFruitCompleteAnimation));
        sequence.Append(greatText.rectTransform.DOScale(Vector3.zero, greatTextAnimationDuration).SetEase(Ease.OutBounce).SetDelay(0.5f));

        for (int i = 0; i < fruitCompleteParticleEffects.Count; i++)
        {
            ParticleSystem effect = Instantiate(fruitCompleteParticleEffects[i], fruitPosition, Quaternion.identity);
        }

        GameManager.SwitchCurrentLevelStat(LevelStats.OnFinalWinScene);
    }
 
    private void PlayLevelPassedAnimations()
    {
        Debug.Log("CLOUD");
        Debug.Log("CONFETTI");
    }
}

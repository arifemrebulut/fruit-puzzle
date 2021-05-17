using UnityEngine;
using DG.Tweening;

public class FinishedFruitAnimations : MonoBehaviour
{
    [SerializeField] float slowRotationAmount, slowRotationDuration;
    [SerializeField] float fastRotationAmount, fastRotationDuration;
    [SerializeField] float yMoveValue, yMoveDuration;
    [SerializeField] float scaleDuration;
    [SerializeField] Vector3 scaleAmount;

    #region Subscribing and Unsubcribing to events for play animations;

    private void OnEnable()
    {
        EventBroker.OnFinalWinScene += PlayFinishedFruidAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnFinalWinScene -= PlayFinishedFruidAnimations;
    }

    #endregion

    private void PlayFinishedFruidAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(0f, slowRotationAmount, 0f),
            slowRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        sequence.Append(transform.DOMoveY(yMoveValue, yMoveDuration).SetEase(Ease.OutQuad));

        sequence.Join(transform.DORotate(new Vector3(0f, slowRotationAmount, 0f),
            slowRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        sequence.Append(transform.DORotate(new Vector3(0f, fastRotationAmount, 0f),
            fastRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic));

        sequence.Join(transform.DOScale(scaleAmount, scaleDuration).SetLoops(4, LoopType.Yoyo));

        sequence.Append(transform.DOMoveY(0f, yMoveDuration * 2).SetEase(Ease.OutQuad));

        sequence.Join(transform.DORotate(new Vector3(0f, slowRotationAmount / 2, 0f),
            slowRotationDuration * 2, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
    }
}

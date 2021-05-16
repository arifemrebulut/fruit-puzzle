using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
    #region Animation Fields

    [Header("Jump Animations")]
    [SerializeField] float jumpScaleAnimationDuration;
    [SerializeField] float jumpMoveAnimatiomDuration;
    [SerializeField] float jumpYMoveValue, jumpYScaleValue, jumpXScaleValue;

    [Header("Cant Jump Animations")]
    [SerializeField] float cantJumpAnimationDuration;
    [SerializeField] float cantJumpXMoveValue, cantJumpYMoveValue, cantJumpZMoveValue;
    [SerializeField] float cantJumpRotationAmount;

    [Header("Level Complete Animations")]
    [SerializeField] float levelCompleteYMoveValue;
    [SerializeField] float levelCompleteYMoveDuration, levelCompleteSpinDuration;
    [SerializeField] Vector3 levelCompleteScaleAmount;

    [Header("Fruit Out From Scene Aniation")]
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float targetYValue;
    [SerializeField] float outFromSceneAnimationDuration;

    #endregion

    #region Subscribing and Unsubscribing to events for Playing Animations
    private void OnEnable()
    {
        EventBroker.OnJump += PlayJumpAnimations;
        EventBroker.OnCantJump += PlayCantJumpAnimations;
        EventBroker.OnFlipping += PlayFlippingAnimations;
        EventBroker.OnFruitFullCovered += PlayFruitFullCoveredAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnJump -= PlayJumpAnimations;
        EventBroker.OnCantJump -= PlayCantJumpAnimations;
        EventBroker.OnFlipping -= PlayFlippingAnimations;
        EventBroker.OnFruitFullCovered -= PlayFruitFullCoveredAnimations;
    }
    #endregion

    private void PlayJumpAnimations()
    {
        transform.DOMoveY(transform.position.y + jumpYMoveValue, jumpMoveAnimatiomDuration).SetLoops(2, LoopType.Yoyo);
        JumpScaleAnimation();
    }

    private void PlayCantJumpAnimations(Vector3 direction)
    {
        transform.DOMoveY(transform.position.y + cantJumpYMoveValue, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);

        JumpScaleAnimation();

        if (direction == Vector3.forward)
        {
            transform.DOMoveZ(transform.position.z + cantJumpZMoveValue, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        }
        if (direction == Vector3.back)
        {
            transform.DOMoveZ(transform.position.z - cantJumpZMoveValue, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        }
        if (direction == Vector3.left)
        {
            transform.DORotate((transform.localEulerAngles + new Vector3(0f, 0f, cantJumpRotationAmount)),
                cantJumpAnimationDuration, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
        }
        if (direction == Vector3.right)
        {
            transform.DORotate((transform.localEulerAngles + new Vector3(0f, 0f, -cantJumpRotationAmount)),
                cantJumpAnimationDuration, RotateMode.Fast).SetLoops(2, LoopType.Yoyo);
        }
    }

    private void PlayFlippingAnimations()
    {
        JumpScaleAnimation();
    }

    private void JumpScaleAnimation()
    {
        transform.DOScaleY(transform.localScale.y + jumpYScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuint);
        transform.DOScaleX(transform.localScale.x + jumpXScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuint);
    }

    private void PlayFruitFullCoveredAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y + levelCompleteYMoveValue,
            levelCompleteYMoveDuration).OnComplete(EventBroker.CallOnLevelComplete).SetEase(Ease.OutQuad)).SetDelay(0.5f);

        sequence.Append(transform.DORotate(new Vector3(0f, 0f, 540f), levelCompleteSpinDuration, RotateMode.LocalAxisAdd));
        sequence.Join(transform.DOScale(transform.localScale + levelCompleteScaleAmount, levelCompleteSpinDuration / 4).SetLoops(4, LoopType.Yoyo));

        sequence.AppendInterval(1.3f);

        sequence.Append(transform.DOMove(targetPosition, outFromSceneAnimationDuration));
        sequence.Join(transform.DORotate(new Vector3(0f, targetYValue, 0f), outFromSceneAnimationDuration));

    }
}

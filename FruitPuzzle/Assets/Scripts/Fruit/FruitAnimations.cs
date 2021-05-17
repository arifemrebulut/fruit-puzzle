using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
    #region Animation Fields

    [Header("Jump Animations")]
    [SerializeField] float jumpScaleDuration;
    [SerializeField] float jumpMoveDuration;
    [SerializeField] float jumpYMoveValue, jumpYScaleValue, jumpXScaleValue;

    [Header("Cant Jump Animations")]
    [SerializeField] float cantJumpAnimationDuration;
    [SerializeField] float cantJumpXMoveValue, cantJumpYMoveValue, cantJumpZMoveValue;
    [SerializeField] float cantJumpRotationAmount;

    [Header("Fruit Complete Animations")]
    [SerializeField] float fruitCompleteYMoveValue;
    [SerializeField] float fruitCompleteYMoveDuration, fruitCompleteSpinDuration;
    [SerializeField] Vector3 fruitCompleteScaleAmount;

    [Header("Fruit Out From Scene Animation")]
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
        EventBroker.OnFruitComplete += PlayFruitCompleteAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnJump -= PlayJumpAnimations;
        EventBroker.OnCantJump -= PlayCantJumpAnimations;
        EventBroker.OnFlipping -= PlayFlippingAnimations;
        EventBroker.OnFruitComplete -= PlayFruitCompleteAnimations;
    }
    #endregion

    private void PlayJumpAnimations()
    {
        transform.DOMoveY(transform.position.y + jumpYMoveValue, jumpMoveDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
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
        transform.DOScaleY(transform.localScale.y + jumpYScaleValue, jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
        transform.DOScaleX(transform.localScale.x + jumpXScaleValue, jumpScaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    }

    private void PlayFruitCompleteAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y + fruitCompleteYMoveValue,
            fruitCompleteYMoveDuration).SetEase(Ease.OutQuad).SetDelay(0.5f));       

        sequence.Append(transform.DORotate(new Vector3(0f, 0f, 540f), fruitCompleteSpinDuration, RotateMode.LocalAxisAdd));
        sequence.Join(transform.DOScale(transform.localScale + fruitCompleteScaleAmount, fruitCompleteSpinDuration / 4)
            .SetLoops(4, LoopType.Yoyo).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.5f);
        sequence.Append(transform.DOMove(targetPosition, outFromSceneAnimationDuration));
        sequence.Join(transform.DORotate(new Vector3(0f, targetYValue, 0f), outFromSceneAnimationDuration));    
    }
}

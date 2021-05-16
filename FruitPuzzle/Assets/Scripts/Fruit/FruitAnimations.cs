using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
    [Header("Jump Animations")]
    [Space(7)]
    [SerializeField] float jumpScaleAnimationDuration;
    [SerializeField] float jumpMoveAnimatiomDuration;
    [SerializeField] float jumpYMoveValue, jumpYScaleValue, jumpXScaleValue;

    [Header("Cant Jump Animations")]
    [Space(7)]
    [SerializeField] float cantJumpAnimationDuration;
    [SerializeField] float cantJumpXMoveValue, cantJumpYMoveValue, cantJumpZMoveValue;
    [SerializeField] float cantJumpRotationAmount;

    [Header("Fruit Complete Animations")]
    [Space(7)]
    [SerializeField] float fruitCompleteYMoveValue;
    [SerializeField] float fruitCompleteYMoveDuration, fruitCompleteSpinDuration;
    [SerializeField] Vector3 fruitCompleteScaleAmount;
    [SerializeField] List<GameObject> fruitCompleteParticleEffects;

    #region Subscribing and Unsubscribing to events for Playing Animations
    private void OnEnable()
    {
        EventBroker.OnJump += PlayJumpAnimations;
        EventBroker.OnCantJump += PlayCantJumpAnimations;
        EventBroker.OnFlipping += PlayFlippingAnimations;
        EventBroker.OnFruitComplete += PlayFruitCompleteAnimation;
    }

    private void OnDisable()
    {
        EventBroker.OnJump -= PlayJumpAnimations;
        EventBroker.OnCantJump -= PlayCantJumpAnimations;
        EventBroker.OnFlipping -= PlayFlippingAnimations;
        EventBroker.OnFruitComplete -= PlayFruitCompleteAnimation;
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
        transform.DOMoveY(transform.position.y + jumpYMoveValue, jumpMoveAnimatiomDuration).SetLoops(2, LoopType.Yoyo);
        JumpScaleAnimation();
    }

    private void JumpScaleAnimation()
    {
        transform.DOScaleY(transform.localScale.y + jumpYScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuint);
        transform.DOScaleX(transform.localScale.x + jumpXScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuint);
    }

    private void PlayFruitCompleteAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y + fruitCompleteYMoveValue,
            fruitCompleteYMoveDuration).SetEase(Ease.OutQuad)).SetDelay(0.5f);

        sequence.Append(transform.DORotate(new Vector3(0f, 0f, 720f), fruitCompleteSpinDuration, RotateMode.LocalAxisAdd));

        sequence.Join(transform.DOScale(transform.localScale + fruitCompleteScaleAmount, fruitCompleteSpinDuration / 4).SetLoops(4, LoopType.Yoyo));
    }
}

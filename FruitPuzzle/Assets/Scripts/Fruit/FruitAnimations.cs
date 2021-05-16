using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
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
    [SerializeField] List<GameObject> levelCompleteParticleEffects;

    #region Subscribing and Unsubscribing to events for Playing Animations
    private void OnEnable()
    {
        EventBroker.OnJump += PlayJumpAnimations;
        EventBroker.OnCantJump += PlayCantJumpAnimations;
        EventBroker.OnFlipping += PlayFlippingAnimations;
        EventBroker.OnLevelComplete += PlayLevelCompleteAnimation;
    }

    private void OnDisable()
    {
        EventBroker.OnJump -= PlayJumpAnimations;
        EventBroker.OnCantJump -= PlayCantJumpAnimations;
        EventBroker.OnFlipping -= PlayFlippingAnimations;
        EventBroker.OnLevelComplete -= PlayLevelCompleteAnimation;
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

    private void PlayLevelCompleteAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveY(transform.position.y + levelCompleteYMoveValue,
            levelCompleteYMoveDuration).SetEase(Ease.OutQuad)).SetDelay(0.5f).OnComplete(PlayLevelCompleteParticleEffects);

        sequence.Append(transform.DORotate(new Vector3(0f, 0f, 540f), levelCompleteSpinDuration, RotateMode.LocalAxisAdd));

        sequence.Join(transform.DOScale(transform.localScale + levelCompleteScaleAmount, levelCompleteSpinDuration / 4).SetLoops(4, LoopType.Yoyo));
    }

    private void PlayLevelCompleteParticleEffects()
    {
        foreach (var effect in levelCompleteParticleEffects)
        {

        }
    }
}

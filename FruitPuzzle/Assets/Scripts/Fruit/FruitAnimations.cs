using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
    [Header("Jump Animation")]
    [Space(20)]
    [SerializeField] float jumpScaleAnimationDuration;
    [SerializeField] float jumpMoveAnimatiomDuration;
    [SerializeField] float jumpYMoveValue, jumpYScaleValue, jumpXScaleValue;

    [Header("Cant Jump Animation")]
    [Space(20)]
    [SerializeField] float cantJumpAnimationDuration;
    [SerializeField] float cantJumpXMoveValue, cantJumpYMoveValue, cantJumpZMoveValue;
    [SerializeField] float cantJumpRotationAmount;
 

    private void OnEnable()
    {
        EventBroker.OnJump += PlayJumpAnimation;
        EventBroker.OnCantJump += PlayCantJumpAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnJump -= PlayJumpAnimation;
        EventBroker.OnCantJump -= PlayCantJumpAnimations;
    }

    private void PlayJumpAnimation()
    {
        transform.DOMoveY(jumpYMoveValue, jumpMoveAnimatiomDuration).SetLoops(2, LoopType.Yoyo);
        JumpScaleAnimation();
    }

    private void PlayCantJumpAnimations(Vector3 direction)
    {
        Vector3 animationDirection;

        transform.DOMoveY((transform.position + new Vector3(0f, cantJumpYMoveValue, 0f)).y, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        JumpScaleAnimation();

        if (direction == Vector3.forward)
        {
            transform.DOMoveZ((transform.position + new Vector3(0f, 0f, cantJumpZMoveValue)).z, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        }
        if (direction == Vector3.back)
        {
            transform.DOMoveZ((transform.position + new Vector3(0f, 0f, -cantJumpZMoveValue)).z, cantJumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
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

    private void JumpScaleAnimation()
    {
        transform.DOScaleY(jumpYScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo);
        transform.DOScaleX(jumpXScaleValue, jumpScaleAnimationDuration).SetLoops(2, LoopType.Yoyo);
    }

    private void PlayLevelCompletedAnimations()
    {
        
    }
}

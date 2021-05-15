using UnityEngine;
using DG.Tweening;

public class FruitAnimations : MonoBehaviour
{
    [Header("Jump Animation")] [Space(10)]
    [SerializeField] float jumpAnimationDuration;
    [SerializeField] float yValueToScale, xValueToScale;

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
        Debug.Log("Play Jump Animation");
        //transform.DOScaleY(yValueToScale, jumpAnimationDuration);
        //transform.DOScaleX(xValueToScale, jumpAnimationDuration);
    }

    private void PlayCantJumpAnimations(Vector3 direction)
    {
        Debug.Log("Play Cant Jump Animation for" + direction);
    }

    private void PlayLevelCompletedAnimations()
    {
        
    }
}

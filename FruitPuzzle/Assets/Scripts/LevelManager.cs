using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("LevelCompleteAnimations")]

    [SerializeField] TextMeshProUGUI greatText;
    [SerializeField] float greatTextAnimationDuration;
    [SerializeField] List<ParticleSystem> levelCompleteParticleEffects;

    private Vector3 fruitPosition;

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

        greatText.rectTransform.DOScale(new Vector3(2f, 2f, 2f), greatTextAnimationDuration).SetEase(Ease.OutBounce);

        for (int i = 0; i < levelCompleteParticleEffects.Count; i++)
        {
            ParticleSystem effect = Instantiate(levelCompleteParticleEffects[i], fruitPosition, transform.rotation);
        }
    }

}

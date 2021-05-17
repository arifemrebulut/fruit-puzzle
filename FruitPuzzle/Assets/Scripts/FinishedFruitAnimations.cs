﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class FinishedFruitAnimations : MonoBehaviour
{
    [SerializeField] float slowRotationAmount, slowRotationDuration;
    [SerializeField] float fastRotationAmount, fastRotationDuration;
    [SerializeField] float yMoveValue, yMoveDuration;
    [SerializeField] float scaleDuration;
    [SerializeField] Vector3 scaleAmount;

    [Header("Particles")]
    [SerializeField] float sugarCreationDuration;
    [SerializeField] List<GameObject> sugarParticles;
    [SerializeField] Transform sugarParticleTransform;

    #region Subscribing and Unsubcribing to events for play animations;

    private void OnEnable()
    {
        EventBroker.OnLevelPassed += PlayFinishedFruidAnimations;
    }

    private void OnDisable()
    {
        EventBroker.OnLevelPassed -= PlayFinishedFruidAnimations;
    }

    #endregion

    private void PlayFinishedFruidAnimations()
    {
        Sequence sequence = DOTween.Sequence();

        StartCoroutine(CreateSugarParticles());

        sequence.Append(transform.DORotate(new Vector3(0f, slowRotationAmount, 0f),
            slowRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));

        sequence.Append(transform.DOMoveY(yMoveValue, yMoveDuration));

        sequence.Join(transform.DORotate(new Vector3(0f, fastRotationAmount, 0f),
            fastRotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic));

        sequence.Join(transform.DOScale(scaleAmount, scaleDuration).SetLoops(4, LoopType.Yoyo));

        sequence.Append(transform.DOMoveY(0f, yMoveDuration * 3).SetEase(Ease.OutQuad)
            .OnStart(EventBroker.CallOnFruitComplete));

        sequence.Join(transform.DORotate(new Vector3(0f, slowRotationAmount / 2, 0f),
            slowRotationDuration * 4, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
    }

    private IEnumerator CreateSugarParticles()
    {
        float elapsedTime = 0f;

        while (elapsedTime < sugarCreationDuration)
        {
            elapsedTime += Time.deltaTime;

            int randomSugarIndex = Random.Range(0, sugarParticles.Count);
            GameObject randomSugar = sugarParticles[randomSugarIndex];

            float randomXPos = Random.Range((sugarParticleTransform.position.x - 0.6f), (sugarParticleTransform.position.x + 0.6f));

            float randomZPos = Random.Range((sugarParticleTransform.position.z - 0.6f), (sugarParticleTransform.position.z + 0.6f));

            Vector3 sugarPosition = new Vector3(randomXPos, 2, randomZPos);
            Vector3 sugarRotation;

            GameObject sugar = Instantiate(randomSugar, sugarPosition, transform.rotation);

            yield return new WaitForSeconds(0.01f);
        }
    }
}

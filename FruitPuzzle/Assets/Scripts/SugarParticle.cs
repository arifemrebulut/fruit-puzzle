using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarParticle : MonoBehaviour
{
    [SerializeField] List<Color> colors;

    Rigidbody rigidbody;
    CapsuleCollider collider;
    MeshRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Color randomColor = colors[Random.Range(0, colors.Count)];

        renderer.material.color = randomColor;
    }

    private void Update()
    {
        if (transform.position.x < -3)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FinishedFruit"))
        {
            collider.enabled = false;   
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.isKinematic = true;
            transform.parent = collision.gameObject.transform;
        }
    }
}

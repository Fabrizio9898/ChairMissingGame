using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    [Header("Distancia antes de las sillas")]
    [SerializeField] private float spaceBeforeChairs = 1f;

    [Header("Objeto que bloquea el centro")]
    [SerializeField] private Transform restrictedCollider;

    public float WallRadius { get; private set; }

    public void Setup(float chairDistance)
    {
        WallRadius = chairDistance - spaceBeforeChairs;

        float diameter = WallRadius * 2f;

        restrictedCollider.localScale =
            new Vector3(diameter, 1f, diameter);
    }
}
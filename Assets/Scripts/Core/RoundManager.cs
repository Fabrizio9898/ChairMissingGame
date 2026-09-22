using Unity.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private IAPlayer enemyPrefab;


    public float ChairDistance { get; private set; }

    [Header("Círculo")]
    [SerializeField] private Transform centerPoint;


    [Header("Distancia desde el centro")]
    [SerializeField] private float distancePerChair = 0.7f;
    [SerializeField] private RestrictedArea restrictedArea;

    public void SetupRound(int chairCount)
    {
        ChairDistance = chairCount * distancePerChair;
        float angleSeparation = 360f / chairCount;
        for (int i = 0; i < chairCount; i++)
        {
            float chairAngle = angleSeparation * i;
            float x = Mathf.Cos(chairAngle * Mathf.Deg2Rad) * ChairDistance;
            float z = Mathf.Sin(chairAngle * Mathf.Deg2Rad) * ChairDistance;
            Vector3 chairPosition = new Vector3(x, 0, z) + centerPoint.position;

            //MIRA HACIA AFUERA
            Vector3 outwardDirection = chairPosition - centerPoint.position;
            Quaternion chairRotation = Quaternion.LookRotation(outwardDirection) * Quaternion.Euler(0, -90f, 0); ;
            Instantiate(chairPrefab, chairPosition, chairRotation);
        }
        restrictedArea.Setup(ChairDistance);



    }

    public IAPlayer CreateAI()
    {
        IAPlayer ai = Instantiate(enemyPrefab);
        return ai;
    }
}
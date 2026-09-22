using Unity.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject chairPrefab;
    [SerializeField] private IAPlayer enemyPrefab;




    [Header("Círculo")]
    [SerializeField] private Transform centerPoint;


    [Header("Distancia desde el centro")]
    [SerializeField] private float distance = 5f;
    public void SetupRound(int chairCount)
    {
        float angleSeparation = 360f / chairCount;
        for (int i = 0; i < chairCount; i++)
        {
            float chairAngle = angleSeparation * i;
            float x = Mathf.Cos(chairAngle * Mathf.Deg2Rad) * distance;
            float z = Mathf.Sin(chairAngle * Mathf.Deg2Rad) * distance;
            Vector3 chairPosition = new Vector3(x, 0, z) + centerPoint.position;
            Instantiate(chairPrefab, chairPosition, Quaternion.identity);
        }


    }

    public IAPlayer CreateAI()
    {
        IAPlayer ai = Instantiate(enemyPrefab);
        return ai;
    }
}
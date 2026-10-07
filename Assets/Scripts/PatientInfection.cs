using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public GameObject wartPrefab;
    public Transform[] wartPoints;

    public void AddWart(int amount)
    {
        amount = Mathf.Min(amount, wartPoints.Length);

        for (int i = 0; i < amount; i++)
        {
            Instantiate(
                wartPrefab,
                wartPoints[i].position,
                wartPoints[i].rotation,
                wartPoints[i]
            );
        }
    }
}
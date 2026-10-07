using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public double infectionSeverity = 0;
    public GameObject wartPrefab;
    public Transform[] wartPoints;

    private void Start()
    {
        StartCoroutine(ProgressInfection());
    }

    public void AddWart(int amount)
    {
        amount = Mathf.Min(amount, wartPoints.Length);

        List<int> availablePoints = new List<int>();

        for (int i = 0; i < wartPoints.Length; i++)
        {
            availablePoints.Add(i);
        }

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);
            int selectedPoint = availablePoints[randomIndex];

            Debug.Log("Wart spawned at point: " + selectedPoint);

            Instantiate(
                wartPrefab,
                wartPoints[selectedPoint].position,
                wartPoints[selectedPoint].rotation,
                wartPoints[selectedPoint]
            );

            availablePoints.RemoveAt(randomIndex);
        }
    }

    IEnumerator ProgressInfection()
    {
        while (infectionSeverity < 4)
        {
            if (infectionSeverity >= 1 && infectionSeverity < 2)
            {
                AddWart(1);
            }
            else if (infectionSeverity >= 2 && infectionSeverity < 3)
            {

            }
            else if (infectionSeverity >= 3 && infectionSeverity < 4)
            {

            }

            infectionSeverity += 0.5;
            yield return new WaitForSeconds(1);
        }
    }
}
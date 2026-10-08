using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public double infectionSeverity = 0;
    public bool beenTreated = false;

    public GameObject wartPrefab;
    public Transform[] wartPoints;

    public List<GameObject> spawnedWarts = new List<GameObject>();

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

            GameObject wart = Instantiate(
                wartPrefab,
                wartPoints[selectedPoint].position,
                wartPoints[selectedPoint].rotation,
                wartPoints[selectedPoint]
            );
            spawnedWarts.Add(wart);

            availablePoints.RemoveAt(randomIndex);
        }
    }

    IEnumerator ProgressInfection()
    {
        while (infectionSeverity < 4 && beenTreated == false)
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
            yield return new WaitForSeconds(2);
        }
    }
    public void Cure(double amount)
    {
        beenTreated = true;
        StartCoroutine(DelayedCure(amount));
    }

    IEnumerator DelayedCure(double amount)
    {
        yield return new WaitForSeconds(5);

        infectionSeverity -= amount;

        if (infectionSeverity < 0)
        {
            infectionSeverity = 0;
        }

        if (infectionSeverity < 1)
        {
            RemoveWarts(2);
        }
        else if (infectionSeverity < 1.5)
        {
            RemoveWarts(1);
        }

        Debug.Log(infectionSeverity);
    }
    void RemoveWarts(int amount)
    {
        amount = Mathf.Min(amount, spawnedWarts.Count);

        for (int i = 0; i < amount; i++)
        {
            GameObject wart = spawnedWarts[spawnedWarts.Count - 1];

            if (wart != null)
            {
                Destroy(wart);
            }

            spawnedWarts.RemoveAt(spawnedWarts.Count - 1);
        }
    }
}
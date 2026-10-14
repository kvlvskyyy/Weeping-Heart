using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public DaySystemsScript DaySystemsScript;

    public double infectionSeverity = 0;
    public bool beenTreated = false;
    public bool lost = false;
    public int cureDay = 0;
    public double cureAmount = 1;

    public GameObject wartPrefab;
    public Transform[] wartPoints;

    public List<GameObject> spawnedWarts = new List<GameObject>();

    private void Update()
    {
        if (DaySystemsScript.isDay == cureDay)
        {
            cureDay = 0;
            infectionSeverity -= cureAmount;
            beenTreated = false;

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

        }
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

    public void ProgressInfection()
    {
        if (!beenTreated)
        {
            if (infectionSeverity >= 1 && infectionSeverity < 2)
            {
                Debug.Log("Warts - stage 1");
                AddWart(1);
            }
            else if (infectionSeverity >= 2 && infectionSeverity < 3)
            {
                Debug.Log("Blossoming - stage 1");
            }
            else if (infectionSeverity >= 3 && infectionSeverity < 4)
            {
                Debug.Log("Weeping Heart - stage 3");
            }
            else if (infectionSeverity >= 4)
            {
                Debug.Log("Lost - stage 4");
                Destroy(gameObject);
                lost = true;
            }
            infectionSeverity += 0.5;
        }
        
    }
    public void Cure(double amount)
    {
        cureAmount = amount;
        beenTreated = true;
        DelayedCure(amount);
    }

    public void DelayedCure(double amount)
    {
        cureDay = DaySystemsScript.isDay + 1;
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
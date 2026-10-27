using System.Collections.Generic;
using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public string patientID;

    private PatientData savedData;

    public GameObject wartPrefab;
    public Transform[] wartPoints;

    public List<GameObject> spawnedWarts = new List<GameObject>();

    private void Start()
    {
        savedData = GameManager.Instance.GetPatientData(patientID);

        if (savedData == null)
        {
            Debug.LogError("problem w " + patientID);
            return;
        }

        RefreshFromData();
    }

    public void RefreshFromData()
    {
        if (savedData == null)
        {
            if (GameManager.Instance == null)
                return;

            savedData = GameManager.Instance.GetPatientData(patientID);

            if (savedData == null)
                return;
        }

        foreach (GameObject wart in spawnedWarts)
        {
            if (wart != null)
                Destroy(wart);
        }

        spawnedWarts.Clear();

        if (savedData.lost)
        {
            gameObject.SetActive(false);
            return;
        }

        int count = Mathf.Min(
            savedData.wartCount,
            wartPoints.Length
        );

        List<int> availablePoints = new List<int>();

        for (int i = 0; i < wartPoints.Length; i++)
        {
            availablePoints.Add(i);
        }

        for (int i = 0; i < count; i++)
        {
            if (availablePoints.Count == 0)
                break;

            int randomIndex = Random.Range(
                0, availablePoints.Count
            );

            int selectedPoint = availablePoints[randomIndex];

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

    public void Cure(double amount)
    {
        if (savedData == null)
            return;

        savedData.cureAmount = amount;
        savedData.beenTreated = true;

        savedData.cureDay = GameManager.Instance.currentDay + 1;
    }
}
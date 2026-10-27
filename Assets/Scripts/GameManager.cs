using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<PatientData> allPatients = new List<PatientData>();

    public int currentDay = 1;
    public int patientsVisited = 0;
    public int maxPatients = 4;

    public int syringes = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 1; i <= 8; i++)
        {
            string id = "patient_" + i.ToString("00");

            if (GetPatientData(id) == null)
            {
                allPatients.Add(new PatientData(id));
            }
        }
    }

    public PatientData GetPatientData(string id)
    {
        return allPatients.Find(p => p.patientID == id);
    }

    public bool CanVisitPatient()
    {
        return patientsVisited < maxPatients;
    }

    public bool RegisterVisit()
    {
        if (!CanVisitPatient())
            return false;

        patientsVisited++;
        return true;
    }

    public void EndDay()
    {
        currentDay++;
        patientsVisited = 0;

        Debug.Log("Day " + currentDay);

        ProgressAllPatients();
    }

    public void ProgressAllPatients()
    {
        foreach (PatientData patient in allPatients)
        {
            if (patient == null || patient.lost)
                continue;
            if (patient.cureDay > 0 &&
                patient.cureDay <= currentDay)
            {
                patient.infectionSeverity =
                    System.Math.Max( 0, patient.infectionSeverity - patient.cureAmount );

                patient.cureDay = 0;
                patient.beenTreated = false;

                patient.wartCount = Mathf.Min(
                    patient.wartCount,
                    GetWartLimit(patient.infectionSeverity)
                );
            }

            if (patient.beenTreated)
                continue;

            patient.infectionSeverity = System.Math.Min(
                4,
                patient.infectionSeverity + 0.5
            );

            if (patient.infectionSeverity >= 1 &&
                patient.infectionSeverity < 2)
            {
                patient.wartCount = Mathf.Min(
                    patient.wartCount + 1, 3
                );
            }

            if (patient.infectionSeverity >= 4)
            {
                patient.lost = true;
            }
        }

        PatientInfection[] activePatients = FindObjectsByType<PatientInfection>();

        foreach (PatientInfection patient in activePatients)
        {
            patient.RefreshFromData();
        }
    }

    private int GetWartLimit(double severity)
    {
        if (severity < 1)
            return 0;

        if (severity < 2)
            return 3;

        return 3;
    }

    // Inventory Logic
    public bool HasSyringe()
    {
        return syringes > 0;
    }

    public bool UseSyringe()
    {
        if (syringes <= 0)
            return false;

        syringes--;
        return true;
    }

    public void AddSyringe(int amount)
    {
        syringes += amount;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<PatientData> allPatients = new List<PatientData>();

    public int currentDay = 1;
    public int patientsVisitedToday = 0;
    public int maxPatientsPerDay = 4;

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
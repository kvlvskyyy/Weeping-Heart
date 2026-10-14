using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DaySystemsScript : MonoBehaviour
{
    public List<PatientInfection> patients = new List<PatientInfection>();

    public int isDay = 0;
    public int maxPatients = 4;
    public int currentPatients = 0;
    public int foodCost = 30;

    public bool canLeave = false;
    public bool canEnter = true;

    public InputAction enterHouse;
    public InputAction leaveHouse;

    private void OnEnable()
    {
        enterHouse.Enable();
        leaveHouse.Enable();
    }

    private void OnDisable()
    {
        enterHouse.Disable();
        leaveHouse.Disable();
    }

    void Start()
    {
        isDay++;
        Debug.Log("It's day " + isDay);
    }

    void Update()
    {
        if (enterHouse.WasPressedThisFrame() && canEnter)
        {
            EnteredHouse();
        }

        if (leaveHouse.WasPressedThisFrame() && canLeave)
        {
            LeftHouse();
        }
    }

    void EnteredHouse()
    {
        currentPatients++;
        Debug.Log(currentPatients);
        Debug.Log("Entered");

        canLeave = true;
        canEnter = false;
    }

    void LeftHouse()
    {
        Debug.Log("Left");

        canLeave = false;
        canEnter = true;

        SurpassPatientAmount();
    }

    void SurpassPatientAmount()
    {
        if (currentPatients >= maxPatients)
        {
            EndDay();
        }
    }

    void EndDay()
    {

        Debug.Log("Day ended");

        foreach (PatientInfection patient in patients)
        {
            if (patient != null)
            {
                patient.ProgressInfection();
            }
        }
        StartDay();
       
    }

    void StartDay()
    {
        currentPatients = 0;
        isDay++;
        Debug.Log("It's day " + isDay);
    }
}
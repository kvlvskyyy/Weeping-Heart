using System;

[Serializable]
public class PatientData
{
    public string patientID;

    public double infectionSeverity = 0;
    public bool beenTreated = false;
    public bool lost = false;

    public int cureDay = 0;
    public double cureAmount = 1;

    public int wartCount = 0;

    public PatientData(string id)
    {
        patientID = id;
    }
}
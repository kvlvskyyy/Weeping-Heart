using System.Collections.Generic;
using UnityEngine;

public class PatientInfection : MonoBehaviour
{
    public string patientID;

    private PatientData savedData;

    public GameObject wartPrefab;
    public Transform[] wartPoints;
    public Transform[] amputableLimbs;

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
        Debug.Log($"[SPAWN] Loading {patientID}. Warts allowed: {savedData.wartCount}. Cure Day is: {savedData.cureDay}");

        foreach (Transform limb in amputableLimbs)
        {
            if (savedData.amputatedLimbs.Contains(limb.name))
            {
                limb.localScale = Vector3.zero;
                if (limb.TryGetComponent<Collider>(out Collider collider)) collider.enabled = false;
            }
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

        while (savedData.activeWartPoints.Count > savedData.wartCount)
        {
            savedData.activeWartPoints.RemoveAt(savedData.activeWartPoints.Count - 1);
        }

        if (savedData.activeWartPoints.Count < savedData.wartCount)
        {

            List<int> availablePoints = new List<int>();

            for (int i = 0; i < wartPoints.Length; i++)
            {
                if (!savedData.activeWartPoints.Contains(i))
                {
                    availablePoints.Add(i);
                }
            }

            int wartsToAdd = savedData.wartCount - savedData.activeWartPoints.Count;
            for (int i = 0; i < wartsToAdd; i++)
            {
                if (availablePoints.Count == 0)
                    break;

                int randomIndex = Random.Range(
                    0, availablePoints.Count
                );

                int selectedPoint = availablePoints[randomIndex];
                
                savedData.activeWartPoints.Add(selectedPoint);
                availablePoints.RemoveAt(randomIndex);
            }
        }

            foreach (int index in savedData.activeWartPoints)
            {
                if (index < wartPoints.Length)
                {
                    GameObject wart = Instantiate(
                        wartPrefab,
                        wartPoints[index].position,
                        wartPoints[index].rotation,
                        wartPoints[index]
                    );

                    spawnedWarts.Add(wart);
                }
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

    public void AmputateLimb(Transform limb)
    {
        limb.localScale = Vector3.zero;
        if (limb.TryGetComponent<Collider>(out Collider collider)) collider.enabled = false;

        if (!savedData.amputatedLimbs.Contains(limb.name))
        {
            savedData.amputatedLimbs.Add(limb.name);
            Debug.Log($"[Amputation] saved limb {limb.name} was removed");
        }
        
        for (int i = spawnedWarts.Count - 1; i >= 0; i--)
        {
            GameObject wart = spawnedWarts[i];

            if (wart.transform.IsChildOf(limb))
            {
                for (int j = 0; j < wartPoints.Length; j++)
                {
                    if (wart.transform.parent == wartPoints[j])
                    {
                        savedData.activeWartPoints.Remove(j);
                        savedData.wartCount = Mathf.Max(0, savedData.wartCount - 1);
                        break;
                    }
                }

                Destroy(wart);
                spawnedWarts.RemoveAt(i);
            }
        }
    }
}
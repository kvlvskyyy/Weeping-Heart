using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DaySystemsScript : MonoBehaviour
{
    public bool isHouseScene = false;
    public string villageSceneName = "Village";

    public InputAction leaveHouse;

    private void OnEnable()
    {
        leaveHouse.Enable();
    }
    private void OnDisable()
    {
        leaveHouse.Disable();
    }
    private void Update()
    {
        if (!isHouseScene)
            return;

        if (leaveHouse.WasPressedThisFrame())
        {
            LeaveHouse();
        }
    }

    private void LeaveHouse()
    {
        GameManager manager = GameManager.Instance;

        if (manager == null)
        {
            Debug.LogError("GameManager is missing!");
            return;
        }

        Debug.Log("Left house");
        Debug.Log(manager.patientsVisitedToday);

        if (manager.patientsVisitedToday >=
            manager.maxPatientsPerDay)
        {
            manager.EndDay();
        }

        SceneManager.LoadScene(villageSceneName);
    }
}
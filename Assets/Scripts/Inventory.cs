using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Syringes
    public float syringes = 0;

    public bool HasSyringe()
    {
        return syringes > 0;
    }

    public bool UseSyringe()
    {
        if (syringes <= 0)
            return false;

        syringes--;
        Debug.Log(syringes);
        return true;
        
    }

    public void AddSyringe(float amount)
    {
        syringes += amount;
        Debug.Log(syringes);
    }
}

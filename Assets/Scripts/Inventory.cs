using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Syringes
    public int syringes = 0;

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

    public void AddSyringe(int amount)
    {
        syringes += amount;
        Debug.Log(syringes);
    }
}

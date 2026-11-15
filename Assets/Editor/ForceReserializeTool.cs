using UnityEditor;

public class ForceReserializeTool
{
    [MenuItem("Tools/Force Reserialize All Assets")]
    public static void ReserializeAll()
    {
        AssetDatabase.ForceReserializeAssets();
    }
}
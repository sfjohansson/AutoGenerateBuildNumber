using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "AutoGenerateBuildNumber/BuildInfoObject", order = 1)]
public class BuildInfoObject : ScriptableObject
{
    public int buildNumber;
    
    static BuildInfoObject _instance;
    
    public static BuildInfoObject Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.Load<BuildInfoObject>("BuildInfoObject");
            }
            
            return _instance;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;

    private void Awake()
    {
        Instance = this;
        
#if UNITY_SERVER || UNITY_STANDALONE
        LoadEnv();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
#if UNITY_SERVER || UNITY_STANDALONE
    #region
    public enum EnvVariable
    {
    }

    private IDictionary environmentVariables;
    private void LoadEnv()
    {
        Debug.LogWarning("GetEnvironmentVariables: ");
        environmentVariables = Environment.GetEnvironmentVariables();
        
        foreach (DictionaryEntry de in environmentVariables)
        {
            Debug.LogWarningFormat("{0}: {1}", de.Key, de.Value);
        }
    }
    #endregion
#endif
}

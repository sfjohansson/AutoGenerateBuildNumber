using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildNumberLabel : MonoBehaviour
{
    public string prefixText = "Build: ";
    
    Text m_text;
    int _displayedNumber = -1;
    
    void Awake()
    {
        m_text = GetComponent<Text>();
    }
    
    void Update()
    {
        if (BuildInfoObject.Instance==null) 
        {
            m_text.text="Fetching";
            return;
        }

        if (_displayedNumber != BuildInfoObject.Instance.buildNumber)
        {
            m_text.text = prefixText+BuildInfoObject.Instance.buildNumber.ToString();
        }
        
    }
}

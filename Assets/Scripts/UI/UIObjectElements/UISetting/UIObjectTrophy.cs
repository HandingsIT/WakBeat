using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectTrophy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickHide()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}

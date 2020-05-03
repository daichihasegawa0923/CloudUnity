using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLoader : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _panels;

    public void ActiveOnePanelByName(string name)
    {
        _panels.ForEach(p => 
        {
            p.gameObject.SetActive(false);
            if (p.name == name)
                p.gameObject.SetActive(true);
        });
    }
}

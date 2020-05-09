using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeter : MonoBehaviour
{
    [SerializeField] protected List<Image> _healthImages;
    
    public int GetHealth()
    {
        var health = 0;
        _healthImages.ForEach(image => health += image.gameObject.activeInHierarchy ? 1 : 0);
        return health;
    }

    public void ChangeHealth(Func<Image,bool> action)
    {
        var isContinue = true;
        _healthImages.ForEach(image =>
        {
            if (!isContinue)
                return;

            isContinue = action(image);
        });
    }

    public void Damage()
    {
        this.ChangeHealth(new Func<Image,bool>(image => 
        {
            if (image.gameObject.activeInHierarchy)
            {
                image.gameObject.SetActive(false);
                return false;
            }
            return true;
        }));
    }

    public void Heal()
    {
        this.ChangeHealth(new Func<Image, bool>(image => 
        {
            if (!image.gameObject.activeInHierarchy)
            {
                image.gameObject.SetActive(true);
                return false;
            }

            return true;
        }));
    }
}

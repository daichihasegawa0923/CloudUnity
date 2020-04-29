using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ControledCharacter>() != null)
            other.GetComponent<ControledCharacter>().Resporn();
    }
}

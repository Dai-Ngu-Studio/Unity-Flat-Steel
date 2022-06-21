using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUltil : MonoBehaviour
{
    // Start is called before the first frame update
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}

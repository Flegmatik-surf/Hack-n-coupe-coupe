using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the different healthbars, to allow them to look at the player
public class HealthBarsController : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform.position);
    }
}

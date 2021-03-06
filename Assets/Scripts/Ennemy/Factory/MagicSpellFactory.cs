using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MagicSpellFactory : GenericFactory<MagicSpell>
{
    // Time after which object will be destroyed
    [SerializeField]
    private float timeout = 10f;
    // Saving enable time to calculate when to destroy itself
    private float startTime;
    /// <summary>
    /// Unity's method called on object enable
    /// </summary>
    private void OnEnable()
    {
        startTime = Time.time;
    }
    /// <summary>
    /// Unity's method called every frame
    /// </summary>
    private void Update()
    {
        // Waiting for timeout
        if (Time.time - startTime > timeout)
        {
            // Destroying object
            Destroy(gameObject);
        }
    }
}

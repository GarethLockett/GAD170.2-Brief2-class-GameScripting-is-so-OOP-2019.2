using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    private void Update()
    {
        // Quick and dirty one-liner to reload the scene (eg use at the end of the battle)
        if( Input.GetKeyDown( KeyCode.Escape ) == true ){ UnityEngine.SceneManagement.SceneManager.LoadScene( 0 ); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{

    public string areaToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;


    


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.storedPosition = playerPosition;
            playerStorage.isStart = false;
            SceneManager.LoadScene(areaToLoad);
        }
    }

    
}


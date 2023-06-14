using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAnim : MonoBehaviour
{

    public Animator OpenDoor;
    

    public IEnumerator startAnim(Collider2D other, float waitTime)
    {
        
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            OpenDoor.SetTrigger("open");   
           
        }
        yield return new WaitForSeconds(waitTime*Time.deltaTime);
    }

    
}

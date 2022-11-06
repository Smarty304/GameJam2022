using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menumanager : MonoBehaviour
{
    public Animator startanim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        startanim.SetTrigger("start");
        StartCoroutine(wait1sec());

        
    }
    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}

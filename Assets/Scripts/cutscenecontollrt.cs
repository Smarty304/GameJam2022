using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutscenecontollrt : MonoBehaviour
{
    public Animator playerAnim;
    public AudioSource Voice;
    public AudioSource Walksound;
    public GameObject denkblase;
    // Start is called before the first frame update
    void Start()
    {
        denkblase.SetActive(false);
        StartCoroutine(walk1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator walk1()
    {
        Walksound.Play();
        playerAnim.SetBool("running", true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Stopnthing());
        
    }
    IEnumerator Stopnthing()
    {
        denkblase.SetActive(true);
        playerAnim.SetBool("running", false);
        yield return new WaitForSeconds(3f);
        StartCoroutine(walk2());
    }
    IEnumerator walk2()
    {
        denkblase.SetActive(false);
        Walksound.Play();
        playerAnim.SetBool("running", true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(2);
    }

}

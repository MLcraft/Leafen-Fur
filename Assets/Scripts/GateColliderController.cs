using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateColliderController : MonoBehaviour
{
    bool leaderComplete = false;
    bool packComplete = false;
    bool levelComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderComplete && packComplete)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Leader")
        {
            leaderComplete = false;
        }
        if (collision.gameObject.name == "Pack")
        {
            packComplete = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Leader")
        {
            leaderComplete = true;
        }
        if (collision.gameObject.name == "Pack")
        {
            packComplete = true;
        }
    }
}

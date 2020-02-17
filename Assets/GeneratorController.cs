using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratorController : MonoBehaviour
{
    public string fox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        fox = collision.gameObject.name;
        SceneManager.LoadScene("Choice", LoadSceneMode.Additive);
        Debug.Log("CHOICE");
        Time.timeScale = 0;
        Destroy(GetComponent<BoxCollider2D>());
    }
}

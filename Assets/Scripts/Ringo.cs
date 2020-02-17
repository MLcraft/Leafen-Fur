using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringo : MonoBehaviour
{
    public GameObject following;
    public bool IsFollowing = false;

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
        Debug.Log(534);
        if (IsFollowing == false)
        {
            if (collision.gameObject.name == "Leader" || collision.gameObject.name == "Pack")
            {
                IsFollowing = true;
                following = collision.gameObject;
            }
        }
    }
}

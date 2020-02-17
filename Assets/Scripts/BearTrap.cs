using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

    public bool trapOpen = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (trapOpen)
        {
            if (collision.gameObject.name == "Leader" || collision.gameObject.name == "Pack")
            {
                trapOpen = false;
                collision.gameObject.SendMessage("Trapped");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {
    int state = 0;
    GameObject player;
    public GameObject fire;
    public GameObject orb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        switch (state) {
            case 0:
                if ((player.transform.position - transform.position).magnitude < 3)
                {
                    state = 1;
                }
                break;
            case 1:
                transform.localEulerAngles += new Vector3(0, 1.0f, 0);
                break;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            
            DestroyObject(GameObject.FindGameObjectWithTag("Bullet"));
            Destroy(gameObject, 1.5f);
        }

    }
}

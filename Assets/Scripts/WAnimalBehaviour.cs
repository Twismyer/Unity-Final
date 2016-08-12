using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WAnimalBehaviour : MonoBehaviour {
    PlayerBehavior playerscript;
    public Transform block;
    public Transform WaterAnimal;
    Vector3 wanderpoint;
    //int wandertime = 3;
    public float Gspeed;
    bool arrived;
    public float closeenough;

    void Start ()
    {
        playerscript = GameObject.Find("Main Camera").GetComponent<PlayerBehavior>();
        Gspeed = .08f;
        closeenough = .3f;
        StartCoroutine(MyCoroutine());
    }
    //Here is a link to an example wander routine
    //http://wiki.unity3d.com/index.php/Wander
    //don't forget to use the array of water block positions in PlayerBehaviour.cs
    // Update is called once per frame

    void Update () {
        StartCoroutine(GeeseMovement());
    }

    IEnumerator GeeseMovement()
    {
        Vector3 target = new Vector3(wanderpoint.x, (playerscript.surface + (WaterAnimal.localScale.y)), wanderpoint.z);
        if (Vector3.Distance(transform.position, target) <= closeenough)
            arrived = true;
        while (Vector3.Distance(transform.position, target) > closeenough)
        {
            //transform.position = Vector3.Lerp(transform.position, target, 1f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target, Gspeed * Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator MyCoroutine()
    {
        while(true)
        {
            if(arrived==true)
                wanderpoint = playerscript.waterblockspos[Random.Range(0, (playerscript.waterblockspos.Count))];
            arrived = false;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

    }
}

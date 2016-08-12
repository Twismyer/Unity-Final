using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    Creation createscript;
    public float speed;
    public GameObject player;
    float moveVertical;
    float moveHorizontal;
    Vector3 playposition;
    Vector3 forward;
    Vector3 tmpForward;
    Vector3 right;
    Vector3 moveDirection;
    Vector3 movement;
    CharacterController playerbody;

    // Use this for initialization
    void Start () {
        createscript = GameObject.Find("Main Camera").GetComponent<Creation>();
        playerbody = player.GetComponent<CharacterController>();
        playposition = player.transform.position;
        player.transform.position = new Vector3(playposition.x, createscript.surface + .25f, playposition.z);
        if (speed == 0)
            speed = 6.0f;
    }
	
	// Update is called once per frame
	void Update () {
        tmpForward = Camera.main.transform.forward;
        forward = new Vector3(tmpForward.x, 0, tmpForward.z);
        right = new Vector3(forward.z, 0, -forward.x);
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        if ((Input.GetKey("d")) && Input.GetKey("a"))
        {
            moveHorizontal = 0;
        }
        if ((Input.GetKey("s")) && Input.GetKey("w"))
        {
            moveVertical = 0;
        }
    }

    void FixedUpdate(){
        Vector3 targetDirection = moveHorizontal * right + moveVertical * forward;
        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
        //movement = new Vector3(moveHorizontal, 0f, moveVertical);
        //movement = Vector3.RotateTowards(movement, transform.localEulerAngles, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
        movement = moveDirection * speed * Time.deltaTime;
        playerbody.Move(movement);
    }
}

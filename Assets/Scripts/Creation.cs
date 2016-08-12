using UnityEngine;
using System.Collections;

public class Creation : MonoBehaviour {
    public Transform block;
    public GameObject ground;
    public int blocknum = 0;
    private const float GROUND_SCALE_WIDTH = 10.0f;
    private const float GROUND_SCALE_HEIGHT = 10.0f;
    public float ground_length;
    public float ground_width;
    public int blocklevels = 3;
    public float surface;
    // Use this for initialization
    Vector3 groundPosition;
    public Vector3 startPoint;
    void Start()
    {
        groundPosition = ground.transform.position;
        ground_length = GROUND_SCALE_HEIGHT * ground.transform.localScale.z;
        ground_width = GROUND_SCALE_WIDTH * ground.transform.localScale.x;
        startPoint = groundPosition + new Vector3(-ground_width/2, 0, ground_length/2);

        for (float x = 0; x < ground_width; x = x + block.localScale.x)
        {
            for (float z = 0; z < ground_length; z = z + block.localScale.z)
            {
                for (float y = 0; y < (blocklevels*block.localScale.y); y = y + block.localScale.y)
                {
                    Instantiate(block, new Vector3(startPoint.x + x + block.localScale.x / 2, y + block.localScale.y / 2, startPoint.z - z - block.localScale.z / 2), Quaternion.identity);
                    blocknum = blocknum + 1;
                }
            }
        }
        surface = blocklevels * block.localScale.y;
    }

    // Update is called once per frame
    void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour {
    public GameObject target;
    public Transform block;
    Creation createscript;
    public float horizontal;
    public float vertical;
    public float check;
    public float sensitivity = 3f;
    public int tool = 0;
    public float maxdig = 3f;
    public float currdig = .9f;
    public int waterblocks = 0;
    public int groundblocks;
    public Text challengetext;
    public Text currencytext;
    public Text slot1text;
    public Image shovel;
    public int currency = 50;
    int challengecount = 0;
    float camspeedmax = 10;
    float blockscale;
    public float surface;
    Vector3 blockcollision;
    bool dwncammove = true;
    bool upcammove = true;
    bool completed = false;
    int blocklevel;
    public List<Vector3> waterblockspos = new List<Vector3>();
    Color ground = new Color(0.176f, 0.270f, 0.027f, 1f);
    Color water = new Color(0.027f, 0.2f, 0.271f, 0.120f);
    Material gameobjectrenderer;
    Vector3 offset;
    AnimalUpdate updateanimalscript;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        createscript = GetComponent<Creation>();
        groundblocks = createscript.blocknum;
        blockscale = block.localScale.y;
        blocklevel = createscript.blocklevels;
        surface = createscript.surface;
        updateanimalscript = GetComponent<AnimalUpdate>();
        SetChallengeText();
        SetCurrencyText(0);
        slot1text.text = "1\nShovel\nSize: " + ((currdig * 10) - 6.5).ToString("n1");
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonDown(0))
        {
            if (tool == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(target.transform.position, currdig);
                foreach (var collision in hitColliders)
                {
                    blockcollision = collision.gameObject.transform.localPosition;
                    if ((blockcollision.y < (surface)) &&
                        (blockcollision.y > 0))
                    {
                        gameobjectrenderer = collision.gameObject.GetComponent<Renderer>().material;
                        if (gameobjectrenderer.GetColor("_Color") != water)
                        {
                            gameobjectrenderer.SetColor("_Color", water);
                            gameobjectrenderer.SetFloat("_Mode", 2);
                            gameobjectrenderer.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                            gameobjectrenderer.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                            gameobjectrenderer.SetInt("_ZWrite", 0);
                            gameobjectrenderer.DisableKeyword("_ALPHATEST_ON");
                            gameobjectrenderer.EnableKeyword("_ALPHABLEND_ON");
                            gameobjectrenderer.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                            gameobjectrenderer.renderQueue = 3000;

                            groundblocks = groundblocks - 1;
                            waterblocks = waterblocks + 1;

                            if (blockcollision.y > ((blocklevel - 1) * blockscale))
                            {
                                waterblockspos.Add(blockcollision);
                            }
                        }
                    }
                }
                updateanimalscript.check();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (tool == 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(target.transform.position.x, surface - (2 * block.localScale.y), target.transform.position.z), currdig);
                foreach (var collision in hitColliders)
                {
                    blockcollision = collision.gameObject.transform.localPosition;
                    if ((blockcollision.y < (surface)) &&
                        (blockcollision.y > 0))
                    {
                        gameobjectrenderer = collision.gameObject.GetComponent<Renderer>().material;
                        if (gameobjectrenderer.GetColor("_Color") == water)
                        {
                            gameobjectrenderer.SetColor("_Color", ground);
                            gameobjectrenderer.SetFloat("_Mode", 0);
                            gameobjectrenderer.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                            gameobjectrenderer.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                            gameobjectrenderer.SetInt("_ZWrite", 1);
                            gameobjectrenderer.DisableKeyword("_ALPHATEST_ON");
                            gameobjectrenderer.DisableKeyword("_ALPHABLEND_ON");
                            gameobjectrenderer.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                            gameobjectrenderer.renderQueue = -1;

                            waterblocks = waterblocks - 1;
                            groundblocks = groundblocks + 1;
                            if (blockcollision.y > ((blocklevel - 1) * blockscale))
                            {
                                for (int i = 0; i < waterblockspos.Count; ++i)
                                {
                                    waterblockspos.Remove(blockcollision);
                                }
                            }
                        }
                    }
                }
                updateanimalscript.check();
            }
        }
        else if (scroll != 0f)
        {
            if ((scroll > 0f) && (currdig < maxdig))
            {
                currdig = currdig + 0.05f;
            }
            else if ((scroll < 0f) && (currdig > .75))
            {
                currdig = currdig - 0.05f;
            }
            slot1text.text = "1\nShovel\nSize: " + ((currdig*10)-6.5).ToString("n1") ;
        }
        if (Input.GetKeyDown("1"))
        {
            if (tool == 0)
            {
                shovel.color = new Color(1f, 1f, .52f);
                tool = 1;
            }
            else if(tool == 1)
            {
                tool = 0;
                shovel.color = new Color(.74f, .74f, .74f);
            }
        }
    }

    void LateUpdate()
    {
        //offset = target.transform.position - transform.position;
        horizontal = Input.GetAxis("Mouse X") * sensitivity;
        vertical = Input.GetAxis("Mouse Y") * sensitivity;
        if (vertical < -camspeedmax)
            vertical = -camspeedmax;
        else if (vertical > camspeedmax)
            vertical = camspeedmax;
        if (horizontal < -camspeedmax)
            horizontal = -camspeedmax;
        else if (horizontal > camspeedmax)
            horizontal = camspeedmax;
        if ((vertical < 0) && (dwncammove == false))
            vertical = 0;
        else if ((vertical > 0) && (upcammove == false))
            vertical = 0;
        /*Quaternion rotation = Quaternion.Euler(vertical, horizontal , 0);
        transform.position = target.transform.position - (rotation * offset);*/
        transform.RotateAround(target.transform.position, transform.right, vertical);
        transform.RotateAround(target.transform.position, transform.up, horizontal);
        check = transform.position.y;
        if (transform.position.y <= 1.5)
        {
            dwncammove = false;
        }
        else if (transform.position.y > 4.25)
        {
            upcammove = false;
        }
        else
        {
            dwncammove = true;
            upcammove = true;
        }
        transform.LookAt(target.transform);
    }

    public void SetChallengeText()
    {
        if (completed == false)
        {
            challengecount = updateanimalscript.Geese.Count;
            challengetext.text = "Challenge: \nAttract 3 water fowl. " + challengecount.ToString() + "/3";
            if (challengecount >= 3)
            {
                challengetext.text = "Congratulations! \nReward 300 credits.";
                completed = true;
                SetCurrencyText(300);
            }
        }
    }

    public void SetCurrencyText(int num)
    {
        currency = currency + num;
        currencytext.text = currency.ToString() + "C";
    }
}

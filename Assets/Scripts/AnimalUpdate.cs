using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalUpdate : MonoBehaviour {
    PlayerBehavior playerscript;
    int idealwaterGoose = 200;
    int MaxTypeAnimal = 3;
    int compare;
    int compareminus;
    public Transform WaterAnimal;
    //public Object[] Geese;
    public List<Object> Geese = new List<Object>();
    // Use this for initialization
    void Start()
    {
        playerscript = GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void check()
    {
        compare = ((playerscript.waterblocks / (Geese.Count + 1)) / idealwaterGoose);
        if (compare > MaxTypeAnimal)
            compare = MaxTypeAnimal;
        if ((Geese.Count < MaxTypeAnimal) && (compare > 0))
        {
            for (; compare > 0; --compare)
            {
                Object temp = Instantiate(WaterAnimal, new Vector3(0, (playerscript.surface + (WaterAnimal.localScale.y)), 0), Quaternion.identity);
                Geese.Add(temp);
                playerscript.SetChallengeText();
            }
        }
        else if ((Geese.Count > 0) && ((playerscript.waterblocks / Geese.Count) < idealwaterGoose))
        {
            compareminus = (Geese.Count - (playerscript.waterblocks / idealwaterGoose));
            for(;compareminus > 0; --compareminus)
            {
                Destroy((Geese[(Geese.Count - 1)] as Transform).gameObject);
                Geese.RemoveAt((Geese.Count - 1));
                playerscript.SetChallengeText();
            }
        }
    }
}

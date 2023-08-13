using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRow : MonoBehaviour
{

    [Range(0.1f, 5f)]
    public float Speed;

    [Range(-0f, 1f)]
    public float UpperEnd;
    [Range(-4f, -2f)]
    public float LowerEnd;

    public bool LowerMiddle;

    private int Direction = 1;
    private bool SpikesLowered;
    private Vector3 DefaultPos;
    private GameObject Spike1;
    private GameObject Spike2;
    private GameObject Spike3;
    private GameObject Spike4;
    // Start is called before the first frame update
    void Start()
    {
        DefaultPos = transform.position;
        Spike1 = transform.GetChild(6).gameObject;
        Spike2 = transform.GetChild(7).gameObject;
        Spike3 = transform.GetChild(8).gameObject;
        Spike4 = transform.GetChild(9).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if((transform.position.y >= UpperEnd + DefaultPos.y && Direction > 0) ||
            (transform.position.y <= LowerEnd + DefaultPos.y && Direction < 0))
        {
            Direction *= -1;
        }

        transform.Translate(0, Speed * Direction * Time.deltaTime, 0);

        if (!LowerMiddle || GameMaster.Player == null)
            return;

        if(SpikesLowered)
        {
            if(Vector3.Distance(DefaultPos, GameMaster.Player.transform.position) > 4f)
            {
                Spike1.SetActive(true);
                Spike2.SetActive(true);
                Spike3.SetActive(true);
                Spike4.SetActive(true);
                SpikesLowered = false;
                //StartCoroutine(MoveSpikes(transform.GetChild(7).gameObject, transform.GetChild(8).gameObject, 0, 1));
            }
        }

        else
        {
            if (Vector3.Distance(DefaultPos, GameMaster.Player.transform.position) <= 4f)
            {
                Spike1.SetActive(false);
                Spike2.SetActive(false);
                Spike3.SetActive(false);
                Spike4.SetActive(false);
                SpikesLowered = true;
                //StartCoroutine(MoveSpikes(transform.GetChild(7).gameObject, transform.GetChild(8).gameObject, -5, -1));

            }
        }
    }
    IEnumerator MoveSpikes(GameObject spike1, GameObject spike2, int target, int direction)
    {
        while((!SpikesLowered && spike1.transform.position.y > target)
            || (SpikesLowered && spike1.transform.position.y < target))
        {
            spike1.transform.Translate(0, Speed * 0.1f * direction, 0);
            spike2.transform.Translate(0, Speed * 0.1f * direction, 0);
            yield return new WaitForSeconds(.1f);
        }
    }
}

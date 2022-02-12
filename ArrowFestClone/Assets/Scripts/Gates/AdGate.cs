using UnityEngine;
using UnityEngine.UI;
using System;

public class AdGate : MonoBehaviour
{
    [SerializeField] private int arrowCount;

    bool isCollide;

    public Action Reaction;

    [Header("0-Ad / 1-Remove / 2-Divide / 3-Multiply")]
    [Range(0, 3)]
    public int situtaion;

    public Text gateText;
    private void Awake()
    {
        switch (situtaion)
        {
            case 0:
                Reaction = Ad;
                gateText.text = "+" + arrowCount;
                break;
            case 1:
                Reaction = Remove;
                gateText.text = "-" + arrowCount;
                break;
            case 2:
                Reaction = Divide;
                gateText.text = "/" + arrowCount;
                break;
            case 3:
                Reaction = Multiply;
                gateText.text = "x" + arrowCount;
                break;
            default:
                Debug.LogError("Situation has to be in (0,1,2,3)");
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            if (!isCollide)
            {
                isCollide = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                Reaction();
            }
        }
    }

    private void Ad()
    {
        ArrowParentCollider.instance.AdArrow(arrowCount);
    }

    private void Remove()
    {
        ArrowParentCollider.instance.RemoveArrow(arrowCount);
    }

    private void Divide()
    {
        ArrowParentCollider.instance.DivideArrow(arrowCount);
    }
    private void Multiply()
    {
        ArrowParentCollider.instance.MultiplyArrow(arrowCount);
    }
}

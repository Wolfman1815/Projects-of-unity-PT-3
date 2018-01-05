using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EasterEggs : MonoBehaviour
{

    // Use this for initialization
    public GameObject target;
    private bool home = true;
    private Vector3 homePos;

    private Vector2 moveDirection;


    public float chaseTriggerDistance = 1.0f;


    public bool successfulAnswer;
    public bool unsuccessfulAnswer;

    public Text questionText;

    public float questionTimer;
    public float bonusTime;

    public bool vacinity;

    void Start()
    {
        homePos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float distanceToPlayer = 0.0f;
        Vector3 playerPosition = target.transform.position;
        moveDirection = new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y);
        distanceToPlayer = moveDirection.magnitude;


        if (distanceToPlayer < chaseTriggerDistance)
        {


            vacinity = true;

            gameObject.GetComponent<Canvas>().enabled = true;





        }
    }

}



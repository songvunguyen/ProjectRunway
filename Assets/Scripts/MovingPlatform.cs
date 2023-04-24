using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startPos;
    public Transform[] wayPoints;

    private int ind;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[startPos].position;
        ind = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, wayPoints[ind].position) < 0.02f)
        {
            ind++;
            if (ind == wayPoints.Length)
            {
                ind = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[ind].position, speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }

    }
}

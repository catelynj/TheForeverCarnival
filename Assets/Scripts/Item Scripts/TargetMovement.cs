using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    // random movement variables
    public float speed;
    private Vector3 moveDirection;
    private Vector3 startPosition;

    private float minX = -1;
    private float maxX = 1;
    private float minY = -1;
    private float maxY = 1;

    private void Start()
    {
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        moveDirection = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

        StartCoroutine(RandomMove());
    }

    // Update is called once per frame
    void Update()
    {

        var newPosition = transform.position + (moveDirection * speed * Time.deltaTime);

        transform.position = newPosition;
    }

    IEnumerator RandomMove()
    {
        yield return new WaitForSeconds(1);

        moveDirection = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

        if(transform.position.x > -30f || transform.position.x < -242f)
        {
            //Debug.Log("x" + transform.position.z);
            transform.position = startPosition;
        }

        if(transform.position.y > 2.1f || transform.position.y < 0.8f)
        {
            Debug.Log("y" + transform.position.y);
            transform.position = startPosition;
        }

        StartCoroutine (RandomMove());
    }
}

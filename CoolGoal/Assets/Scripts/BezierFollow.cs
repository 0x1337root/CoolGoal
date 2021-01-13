using System.Collections;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 ballPosition;

    private Vector3 startPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 1.25f;
        coroutineAllowed = true;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && Input.mousePosition.y < 600)
        {
            if (coroutineAllowed)
            {
                StartCoroutine(GoByTheRoute(routeToGo));
            }
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            ballPosition = Mathf.Pow(1 - tParam, 2) * p0 +
                        2 * Mathf.Pow(1 - tParam, 1) * tParam * p1 +
                        Mathf.Pow(tParam, 2) * p2;

            transform.position = ballPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        transform.position = startPosition;

        if(routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }
}
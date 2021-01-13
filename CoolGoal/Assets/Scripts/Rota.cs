using UnityEngine;

public class Rota : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector3 gizmosPosition;

    private Vector3 mPosition;

    private Vector3 p1Pos;

    private Vector3 p2Pos;

    private float x1;

    private float x2;

    private float x3;

    private void Start()
    {
        p1Pos = controlPoints[1].position;
        p2Pos = controlPoints[2].position;
    }

    private void FixedUpdate()
    {
        mPosition = Input.mousePosition;

        x2 = (mPosition.x - x1) / 60f;
        x3 = (mPosition.x - x1) / 115.18f;

        p1Pos.x = x2;
        p2Pos.x = x3;

        if (p1Pos.x >= 4.5f)
        {
            p1Pos.x = 4.5f;
        }

        else if(p1Pos.x <= -4.5f)
        {
            p1Pos.x = -4.5f;
        }

        if(p2Pos.x >= 3.5f)
        {
            p2Pos.x = 3.5f;
        }

        else if(p2Pos.x <= -3.5f)
        {
            p2Pos.x = -3.5f;
        }

        controlPoints[1].position = p1Pos;
        controlPoints[2].position = p2Pos;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = mPosition.x;
        }
    }

    private void OnDrawGizmos()
    {
        if (Input.GetMouseButton(0))
        {
            if (mPosition.y < 600)
            {
                for (float t = 0.05f; t <= 1; t += 0.05f)
                {
                    gizmosPosition = Mathf.Pow(1 - t, 2) * controlPoints[0].position +
                        2 * Mathf.Pow(1 - t, 1) * t * controlPoints[1].position +
                        Mathf.Pow(t, 2) * controlPoints[2].position;

                    Gizmos.color = Color.magenta;
                    Gizmos.DrawSphere(gizmosPosition, 0.1f);
                }
            }
        }
    }
}
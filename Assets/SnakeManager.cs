 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    List<Transform> body = new List<Transform>();
    public Transform bodyPrefab;
    public float speed = 5.0f;
    float moveTime = 0;
    Vector2 direction = Vector3.up;
    Vector2 snakeIndex;
    private void Update()
    {

    }

    void Movimento()
    {
        if (Time.time > moveTime)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].position = body[i - 1].position;
            }
            if (body.Count > 0)
            {
                body[0].position = (Vector2)transform.position;
            }
            transform.position += (Vector3)direction * paredemanage.instance.tCelula;
            moveTime = Time.time + 1 / speed;
            snakeIndex = transform.position / paredemanage.instance.tCelula;

        }
    }
}

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

    List<Transform> food = new List<Transform>();
    private void Update()
    {
        Movimento();
        ChangeDirection();

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
    void ChangeDirection()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (input.y == -1)
        {
            direction = Vector2.down;
        }
        else if (input.y == 1)
        {
            direction = Vector2.up;
        }
        else if (input.x == -1)
        {
            direction = Vector2.left;
        }
        else if (input.x == 1)
        {
            direction = Vector2.right;
        }
    }

    void Corpo()
    {
        Vector2 position = transform.position;

        if (body.Count != 0)
        {
            position = body[body.Count - 1].position;
        }
        body.Add(Instantiate(bodyPrefab, position, Quaternion.identity).transform);
    }

    void Comer()
    {
        for (int i = 0; i < food.Count; i++)
        {
            Vector2 foodIndex = food[i].position / paredemanage.instance.tCelula;
            if (Mathf.Abs(foodIndex.x - snakeIndex.x) < 0.00001f && Mathf.Abs(foodIndex.y - snakeIndex.y) < 0.00001f)
            {
                Destroy(food[i].gameObject);
                food.Remove(food[i]);
                Corpo();
                ptsetela.instance.pontos++;
                ptsetela.instance.ponto.text = "Pontuação: " + ptsetela.instance.pontos.ToString();

            }

        }
    }
}

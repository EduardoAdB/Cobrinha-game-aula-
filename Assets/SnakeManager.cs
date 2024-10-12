using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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
        Movimento();
        ChangeDirection();
        Comer();
        CheckBodyCollision();
        Teleporte();
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
            transform.position += (Vector3)direction * wallManager.instance.tCelula;
            moveTime = Time.time + 1 / speed;
            snakeIndex = transform.position / wallManager.instance.tCelula;

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
        if (wallManager.instance.fruta.transform.position == transform.position)
        {
            Debug.Log("colidiu");
            Destroy(wallManager.instance.fruta.gameObject);
            wallManager.instance.Frutas();
            Corpo();
            ptsetela.instance.pontos++;

        }
    }
    void CheckBodyCollision()
    {
        if (body.Count < 3) return;
         
        for (int i = 0; i < body.Count; ++i)
        {
            Vector2 index = body[i].position / wallManager.instance.tCelula;
            if (Mathf.Abs(index.x - snakeIndex.x) < 0.00001f && Mathf.Abs(index.y - snakeIndex.y) < 0.00001f)
            {
                ptsetela.instance.GameOver();
                break;
            }
        }
    }

    void Teleporte()
    {
        Vector2 pos = transform.position;

        // Calcula os limites do mapa com base nas linhas e colunas e no tamanho da célula
        float limiteEsquerdo = 0f;
        float limiteDireito = (wallManager.instance.colunas - 1) * wallManager.instance.tCelula;
        float limiteInferior = 0f;
        float limiteSuperior = (wallManager.instance.linhas - 1) * wallManager.instance.tCelula;

        // Verifica se a posição X da cobra ultrapassa os limites horizontais
        if (pos.x > limiteDireito)
        {
            pos.x = limiteEsquerdo; // Teletransporta para o lado esquerdo
        }
        else if (pos.x < limiteEsquerdo)
        {
            pos.x = limiteDireito; // Teletransporta para o lado direito
        }

        // Verifica se a posição Y da cobra ultrapassa os limites verticais
        if (pos.y > limiteSuperior)
        {
            pos.y = limiteInferior; // Teletransporta para a parte inferior
        }
        else if (pos.y < limiteInferior)
        {
            pos.y = limiteSuperior; // Teletransporta para a parte superior
        }

        // Atualiza a posição da cobra após o teleporte (centralizando nas células)
        transform.position = new Vector2(Mathf.Round(pos.x / wallManager.instance.tCelula) * wallManager.instance.tCelula,
                                         Mathf.Round(pos.y / wallManager.instance.tCelula) * wallManager.instance.tCelula);
    }

}
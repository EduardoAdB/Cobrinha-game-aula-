using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public static SnakeManager instance; // Singleton para f�cil acesso
    List<Transform> body = new List<Transform>();
    public Transform bodyPrefab;
    public float speed = 5.0f; // Velocidade ajust�vel pelo jogador
    float moveTime = 0;
    Vector2 direction = Vector3.up;
    Vector2 snakeIndex;
    private bool isGameActive = false; // O jogo come�a inativo

    void Awake()
    {
        instance = this; // Define este script como singleton
    }

    private void Update()
    {
        if (isGameActive) // S� atualiza se o jogo estiver ativo
        {
            Movimento();
            ChangeDirection();
            Comer();
            CheckBodyCollision();
            Teleporte();
        }
    }

    public void StartGame()
    {
        // Ativa o jogo e reseta o estado inicial
        isGameActive = true;
        ResetSnake();
        Time.timeScale = 1f; // Reinicia o tempo de jogo se estiver pausado
    }

    public void StopGame()
    {
        isGameActive = false; // Para o jogo
        Time.timeScale = 0f; // Pausa o tempo de jogo
    }

    void Movimento()
    {
        if (Time.time > moveTime && enabled == true)
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

        Vector2 headPosition = transform.position;

        for (int i = 0; i < body.Count; i++)
        {
            if (Vector2.Distance(headPosition, body[i].position) < 0.01f)
            {
                Debug.Log("Game Over! A cobra colidiu com o corpo.");

                // Parando o jogo
                StopGame();

                // Exibe a tela de game over ou retorna para o menu principal
                ptsetela.instance.Pontuacao(); // Mostra a pontua��o
                return;
            }
        }
    }

    void Teleporte()
    {
        Vector2 pos = transform.position;

        // Calcula os limites do mapa com base nas linhas e colunas e no tamanho da c�lula
        float limiteEsquerdo = 0f;
        float limiteDireito = (wallManager.instance.colunas - 1) * wallManager.instance.tCelula;
        float limiteInferior = 0f;
        float limiteSuperior = (wallManager.instance.linhas - 1) * wallManager.instance.tCelula;

        // Verifica se a posi��o X da cobra ultrapassa os limites horizontais
        if (pos.x > limiteDireito)
        {
            pos.x = limiteEsquerdo; // Teletransporta para o lado esquerdo
        }
        else if (pos.x < limiteEsquerdo)
        {
            pos.x = limiteDireito; // Teletransporta para o lado direito
        }

        // Verifica se a posi��o Y da cobra ultrapassa os limites verticais
        if (pos.y > limiteSuperior)
        {
            pos.y = limiteInferior; // Teletransporta para a parte inferior
        }
        else if (pos.y < limiteInferior)
        {
            pos.y = limiteSuperior; // Teletransporta para a parte superior
        }

        // Atualiza a posi��o da cobra ap�s o teleporte (centralizando nas c�lulas)
        transform.position = new Vector2(Mathf.Round(pos.x / wallManager.instance.tCelula) * wallManager.instance.tCelula,
                                         Mathf.Round(pos.y / wallManager.instance.tCelula) * wallManager.instance.tCelula);
    }

    // Reseta o estado da cobra ao iniciar o jogo novamente
    void ResetSnake()
    {
        transform.position = Vector3.zero;
        direction = Vector2.up;
        foreach (Transform segment in body)
        {
            Destroy(segment.gameObject);
        }
        body.Clear();
    }
}
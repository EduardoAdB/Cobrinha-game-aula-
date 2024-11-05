using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public static SnakeManager instance; // Singleton para fácil acesso
    List<Transform> body = new List<Transform>(); // Lista para armazenar as partes do corpo da cobra
    public Transform bodyPrefab; // Prefab usado para instanciar partes do corpo da cobra
    public float speed = 5.0f; // Velocidade ajustável da cobra
    float moveTime = 0; // Tempo de espera entre movimentos
    Vector2 direction = Vector3.up; // Direção inicial da cobra
    Vector2 snakeIndex; // Índice atual da posição da cobra no grid

    void Awake()
    {
        instance = this; // Define este script como singleton
    }

    private void Update()
    {
        if (ptsetela.instance.jogoIniciado == true) // Checa se o jogo está iniciado
        {
            Movimento();           // Controla o movimento da cobra
            ChangeDirection();      // Muda a direção com base na entrada do jogador
            Comer();                // Checa se a cobra comeu uma fruta
            CheckBodyCollision();   // Verifica colisões com o próprio corpo
            Teleporte();            // Permite teleporte nas bordas do mapa
            return;
        }
    }

    void Movimento()
    {
        // Controla o movimento da cobra e atualiza a posição do corpo
        if (Time.time > moveTime && enabled == true)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].position = body[i - 1].position; // Move cada parte do corpo para a posição da parte anterior
            }
            if (body.Count > 0)
            {
                body[0].position = (Vector2)transform.position; // A primeira parte do corpo segue a cabeça
            }
            transform.position += (Vector3)direction * wallManager.instance.tCelula; // Move a cabeça na direção definida
            moveTime = Time.time + 1 / speed; // Define o tempo do próximo movimento com base na velocidade
            snakeIndex = transform.position / wallManager.instance.tCelula; // Atualiza a posição da cobra no grid
        }
    }

    void ChangeDirection()
    {
        // Muda a direção da cobra com base na entrada do jogador
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
        // Adiciona uma nova parte ao corpo da cobra
        Vector2 position = transform.position;

        if (body.Count != 0)
        {
            position = body[body.Count - 1].position; // Posição da nova parte é a última parte do corpo
        }
        body.Add(Instantiate(bodyPrefab, position, Quaternion.identity).transform); // Instancia nova parte e adiciona à lista
    }

    void Comer()
    {
        // Verifica se a cobra comeu a fruta
        if (wallManager.instance.fruta.transform.position == transform.position)
        {
            Debug.Log("colidiu");
            Destroy(wallManager.instance.fruta.gameObject); // Destrói a fruta
            wallManager.instance.Frutas();                  // Gera uma nova fruta
            Corpo();                                        // Adiciona uma nova parte ao corpo da cobra
            ptsetela.instance.pontos++;                     // Incrementa a pontuação
        }
    }

    void CheckBodyCollision()
    {
        // Checa se a cobra colidiu com seu próprio corpo
        if (body.Count < 3) return;

        Vector2 headPosition = transform.position;

        for (int i = 0; i < body.Count; i++)
        {
            if (Vector2.Distance(headPosition, body[i].position) < 0.01f)
            {
                Debug.Log("Game Over! A cobra colidiu com o corpo.");

                // Para o jogo, exibe a tela de game over e salva a pontuação
                ptsetela.instance.Pontuacao();
                ptsetela.instance.jogoIniciado = false;
                ptsetela.instance.painelFinal.SetActive(true);
            }
        }
    }

    void Teleporte()
    {
        // Permite que a cobra teleporte de uma borda para outra
        Vector2 pos = transform.position;

        float limiteEsquerdo = 0f;
        float limiteDireito = (wallManager.instance.colunas - 1) * wallManager.instance.tCelula;
        float limiteInferior = 0f;
        float limiteSuperior = (wallManager.instance.linhas - 1) * wallManager.instance.tCelula;

        if (pos.x > limiteDireito)
        {
            pos.x = limiteEsquerdo; // Teleporta para o lado esquerdo
        }
        else if (pos.x < limiteEsquerdo)
        {
            pos.x = limiteDireito; // Teleporta para o lado direito
        }

        if (pos.y > limiteSuperior)
        {
            pos.y = limiteInferior; // Teleporta para a parte inferior
        }
        else if (pos.y < limiteInferior)
        {
            pos.y = limiteSuperior; // Teleporta para a parte superior
        }

        // Centraliza a posição da cobra na célula após o teleporte
        transform.position = new Vector2(Mathf.Round(pos.x / wallManager.instance.tCelula) * wallManager.instance.tCelula,
                                         Mathf.Round(pos.y / wallManager.instance.tCelula) * wallManager.instance.tCelula);
    }

    public void ZerarCobra()
    {
        // Destroi todas as partes do corpo da cobra para reiniciar o jogo
        foreach (Transform segment in body)
        {
            Destroy(segment.gameObject);
        }

        // Limpa a lista de partes do corpo e redefine a posição da cabeça
        body.Clear();
        transform.position = new Vector2(0, 0);
        direction = Vector2.up; // Redefine a direção inicial para cima
    }

    public void Velocidade(string _speed)
    {
        // Altera a velocidade da cobra com base na entrada do usuário
        speed = int.Parse(_speed);
    }
}

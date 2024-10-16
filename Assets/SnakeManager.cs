using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    public static SnakeManager instance; // Singleton para fácil acesso
    List<Transform> body = new List<Transform>();
    public Transform bodyPrefab;
    public float speed = 5.0f; // Velocidade ajustável pelo jogador
    float moveTime = 0;
    Vector2 direction = Vector3.up;
    Vector2 snakeIndex;

    void Awake()
    {
        instance = this; // Define este script como singleton
    }

    private void Update()
    {        
        if (ptsetela.instance.jogoIniciado == true)
        {
            Movimento();
            ChangeDirection();
            Comer();
            CheckBodyCollision();
            Teleporte();
        }  
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
                // Exibe a tela de game over ou retorna para o menu principal
                ptsetela.instance.Pontuacao(); // Mostra a pontuação
                ptsetela.instance.jogoIniciado = false;
                ptsetela.instance.painelFinal.SetActive(true);
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

    public void ZerarCobra()
    {       
            // Itera por todos os segmentos do corpo e destrói cada um
            foreach (Transform segment in body)
            {
                Destroy(segment.gameObject);
            }

            // Limpa a lista para remover as referências aos segmentos que foram destruídos
            body.Clear();     
            transform.position = new Vector2 (0, 0);
            direction = Vector2.up;
    }
}
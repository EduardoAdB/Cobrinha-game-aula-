using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ptsetela : MonoBehaviour
{
    #region singleton 
    // Singleton pattern: permite o acesso a uma única instância desta classe em outras partes do código
    static public ptsetela instance;
    #endregion

    // Variáveis para gerenciar a pontuação do jogador
    public int pontos = 0;  // Pontuação atual
    int maxpontos = 0;      // Pontuação máxima

    // Referências para os componentes de interface do usuário (UI) no jogo
    public TextMeshProUGUI ponto;        // Exibe a pontuação atual na tela
    public TextMeshProUGUI pontomax;     // Exibe a pontuação máxima na tela
    public TextMeshProUGUI gameOverText; // Texto de "game over"
    public GameObject panel;             // Painel da tela inicial
    public GameObject painelFinal;       // Painel de final de jogo

    // Estados do jogo
    public bool telaInicial = true;   // Indica se a tela inicial está ativa
    public bool jogoIniciado = false; // Indica se o jogo está em andamento

    // Campos de entrada para personalizar o jogo
    public TMP_InputField inputLinha;       // Campo para definir o número de linhas do mapa
    public TMP_InputField inputColuna;      // Campo para definir o número de colunas do mapa
    public TMP_InputField inputVelocidade;  // Campo para definir a velocidade da cobra


    private void Awake()
    {
        // Configura a instância da classe como um singleton, permitindo que outras partes do código acessem ptsetela.instance.
        instance = this;
    }

    private void Start()
    {
        // Define o estado inicial do jogo. Exibe "0 pontos" tanto na pontuação atual quanto na pontuação máxima na interface.
        telaInicial = true;
        ponto.text = "0 pontos";
        pontomax.text = "0 pontos";

        // Inicializa os campos de entrada com valores da configuração atual do jogo
        // (linhas e colunas do mapa e velocidade da cobra), obtidos de wallManager e SnakeManager.
        inputLinha.text = wallManager.instance.linhas.ToString();
        inputColuna.text = wallManager.instance.colunas.ToString();
        inputVelocidade.text = SnakeManager.instance.speed.ToString();
    }

    private void Update()
    {
        // Atualiza constantemente o texto da pontuação atual na interface com o valor atual de "pontos".
        ponto.text = "pontos atuais: " + pontos.ToString();
    }

    public void Pontuacao()
    {
        // Verifica se a pontuação atual é maior que a pontuação máxima registrada.
        // Se sim, atualiza a pontuação máxima e redefine a pontuação atual para zero.
        if (pontos > maxpontos)
        {
            maxpontos = pontos;
            pontos = 0;
            // Atualiza o texto da pontuação máxima na interface com o novo valor.
            pontomax.text = "Maior pontuação: " + maxpontos.ToString();
        }
    }

    public void IniciarJogo()
    {
        // Inicia o jogo, desativando o painel inicial e o painel final, reiniciando a pontuação e resetando o estado da cobra e do mapa.
        panel.SetActive(false);
        painelFinal.SetActive(false);
        pontos = 0;
        // Reinicia a cobra e o mapa, configura a câmera e gera o mapa inicial com as frutas.
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
        jogoIniciado = true;
        wallManager.instance.Frutas();
        wallManager.instance.AjustarCamera();
        wallManager.instance.GerarMapa();
    }

    public void MenuInicial()
    {
        // Retorna para o menu inicial: reinicia a pontuação, configura o jogo como não iniciado e mostra o painel do menu inicial.
        pontos = 0;
        jogoIniciado = false;
        panel.SetActive(true);
        painelFinal.SetActive(false);
        // Reinicia o estado da cobra e do mapa para uma nova configuração.
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
    }
}
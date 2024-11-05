using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ptsetela : MonoBehaviour
{
    #region singleton 
    // Singleton pattern: permite o acesso a uma �nica inst�ncia desta classe em outras partes do c�digo
    static public ptsetela instance;
    #endregion

    // Vari�veis para gerenciar a pontua��o do jogador
    public int pontos = 0;  // Pontua��o atual
    int maxpontos = 0;      // Pontua��o m�xima

    // Refer�ncias para os componentes de interface do usu�rio (UI) no jogo
    public TextMeshProUGUI ponto;        // Exibe a pontua��o atual na tela
    public TextMeshProUGUI pontomax;     // Exibe a pontua��o m�xima na tela
    public TextMeshProUGUI gameOverText; // Texto de "game over"
    public GameObject panel;             // Painel da tela inicial
    public GameObject painelFinal;       // Painel de final de jogo

    // Estados do jogo
    public bool telaInicial = true;   // Indica se a tela inicial est� ativa
    public bool jogoIniciado = false; // Indica se o jogo est� em andamento

    // Campos de entrada para personalizar o jogo
    public TMP_InputField inputLinha;       // Campo para definir o n�mero de linhas do mapa
    public TMP_InputField inputColuna;      // Campo para definir o n�mero de colunas do mapa
    public TMP_InputField inputVelocidade;  // Campo para definir a velocidade da cobra


    private void Awake()
    {
        // Configura a inst�ncia da classe como um singleton, permitindo que outras partes do c�digo acessem ptsetela.instance.
        instance = this;
    }

    private void Start()
    {
        // Define o estado inicial do jogo. Exibe "0 pontos" tanto na pontua��o atual quanto na pontua��o m�xima na interface.
        telaInicial = true;
        ponto.text = "0 pontos";
        pontomax.text = "0 pontos";

        // Inicializa os campos de entrada com valores da configura��o atual do jogo
        // (linhas e colunas do mapa e velocidade da cobra), obtidos de wallManager e SnakeManager.
        inputLinha.text = wallManager.instance.linhas.ToString();
        inputColuna.text = wallManager.instance.colunas.ToString();
        inputVelocidade.text = SnakeManager.instance.speed.ToString();
    }

    private void Update()
    {
        // Atualiza constantemente o texto da pontua��o atual na interface com o valor atual de "pontos".
        ponto.text = "pontos atuais: " + pontos.ToString();
    }

    public void Pontuacao()
    {
        // Verifica se a pontua��o atual � maior que a pontua��o m�xima registrada.
        // Se sim, atualiza a pontua��o m�xima e redefine a pontua��o atual para zero.
        if (pontos > maxpontos)
        {
            maxpontos = pontos;
            pontos = 0;
            // Atualiza o texto da pontua��o m�xima na interface com o novo valor.
            pontomax.text = "Maior pontua��o: " + maxpontos.ToString();
        }
    }

    public void IniciarJogo()
    {
        // Inicia o jogo, desativando o painel inicial e o painel final, reiniciando a pontua��o e resetando o estado da cobra e do mapa.
        panel.SetActive(false);
        painelFinal.SetActive(false);
        pontos = 0;
        // Reinicia a cobra e o mapa, configura a c�mera e gera o mapa inicial com as frutas.
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
        jogoIniciado = true;
        wallManager.instance.Frutas();
        wallManager.instance.AjustarCamera();
        wallManager.instance.GerarMapa();
    }

    public void MenuInicial()
    {
        // Retorna para o menu inicial: reinicia a pontua��o, configura o jogo como n�o iniciado e mostra o painel do menu inicial.
        pontos = 0;
        jogoIniciado = false;
        panel.SetActive(true);
        painelFinal.SetActive(false);
        // Reinicia o estado da cobra e do mapa para uma nova configura��o.
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
    }
}
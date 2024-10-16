using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ptsetela : MonoBehaviour
{
    #region singleton 
    static public ptsetela instance;
    #endregion

    public int pontos = 0;
    int maxpontos = 0;

    public TextMeshProUGUI ponto;
    public TextMeshProUGUI pontomax;
    public TextMeshProUGUI gameOverText;
    public GameObject panel;
    public GameObject painelFinal;
    
    public bool telaInicial = true;
    public bool jogoIniciado = false;
   
    public TMP_InputField inputLinha;
    public TMP_InputField inputColuna;
    public TMP_InputField inputVelocidade;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        telaInicial = true;
        ponto.text = "0 pontos";
        pontomax.text = "0 pontos";

        inputLinha.text = wallManager.instance.linhas.ToString();
        inputColuna.text = wallManager.instance.colunas.ToString();
        inputVelocidade.text = SnakeManager.instance.speed.ToString();
    }

    private void Update()
    {
        ponto.text = "pontos atuais: " + pontos.ToString();       
    }

    public void Pontuacao()
    {
        if (pontos > maxpontos)
        {
            maxpontos = pontos;
            pontos = 0;
            pontomax.text = "Maior pontuação: " + maxpontos.ToString();
        }

    }

    public void IniciarJogo()
    {
        wallManager.instance.linhas = int.Parse(inputLinha.text);
        wallManager.instance.colunas = int.Parse(inputColuna.text);
        SnakeManager.instance.speed = float.Parse(inputVelocidade.text);

        Debug.Log(wallManager.instance.linhas);
        Debug.Log(wallManager.instance.colunas);
        Debug.Log(SnakeManager.instance.speed);

        panel.SetActive(false);
        painelFinal.SetActive(false);
        pontos = 0;
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
        jogoIniciado = true;
    }
    public void MenuInicial()
    {
        pontos = 0;
        jogoIniciado = false;
        panel.SetActive(true);
        painelFinal.SetActive(false);
        SnakeManager.instance.ZerarCobra();
        wallManager.instance.ZerarMapa();
        wallManager.instance.limitador = true;
    }
}
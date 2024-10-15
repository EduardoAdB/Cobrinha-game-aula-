using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public bool telaInicial = true;

    bool gameOver = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        telaInicial = true;
        ponto.text = "0 pontos";
        pontomax.text = "0 pontos";
    }

    private void Update()
    {
        ponto.text = "pontos atuais: " + pontos.ToString();
        if (Input.GetKeyDown(KeyCode.E))
        {
            telaInicial = false;
        }
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

    public void GameOver()
    {
        gameOver = true; //7:36
        gameOverText.gameObject.SetActive(true);
    }

}
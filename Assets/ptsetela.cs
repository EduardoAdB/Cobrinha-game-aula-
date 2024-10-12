using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ptsetela : MonoBehaviour
{
    #region singleton 
    static public ptsetela instance;
    #endregion

    public int pontos = 0;
    int maxpontos;

    public TextMeshProUGUI ponto;
    public TextMeshProUGUI pontomax;
    public TextMeshProUGUI gameOverText;

    bool gameOver = false;
    private void Awake()
    {
        instance = this;
    }

    void Pontuacao()
    {
        maxpontos = 0;

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
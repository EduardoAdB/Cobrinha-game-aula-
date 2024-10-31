using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class wallManager : MonoBehaviour
{
    #region singleton
    static public wallManager instance;

    #endregion

    public GameObject wall;
    public GameObject fruit;

    public GameObject fruta;
    public int quantidadeFrutas;


    public GameObject[,] mapa;
    public int linhas;
    public int colunas;
    public float tCelula = 1.1f;
    float dCamera = 10f;

    private void Awake()
    {       
        instance = this;
    }
    public void Linhas(string _linha)
    {
        linhas = int.Parse(_linha);
    }

    public void Colunas(string _colunas)
    {
        colunas = int.Parse(_colunas);
    }


    public void GerarMapa()
    {
        Debug.Log("Linhas: " + linhas + ", Colunas: " + colunas);
        mapa = new GameObject[linhas, colunas];
        for (int L = 0; L < linhas; L++) // Itera sobre as linhas
        {
            for (int A = 0; A < colunas; A++) // Itera sobre as colunas
            {
                mapa[L,A] = Instantiate(wall, new Vector2(A * tCelula, L * tCelula), Quaternion.identity);
            }
        }       
    }
    public void AjustarCamera()
    {
        // Calcula a maior dimens�o do mapa para definir o tamanho da c�mera
        float maiorDimensao = Mathf.Max(linhas, colunas);

        // Define uma margem extra para afastar a c�mera
        float margem = 1.5f; // Ajuste este valor para mais ou menos afastamento

        // Ajusta o tamanho ortogr�fico da c�mera para que ela cubra a maior dimens�o + margem
        Camera.main.orthographicSize = (maiorDimensao * tCelula) / 2 + margem;

        // Centraliza a c�mera no centro do mapa
        Vector3 centroMatriz = new Vector3((colunas - 1) * tCelula / 2, (linhas - 1) * tCelula / 2, -dCamera);
        Camera.main.transform.position = centroMatriz;
    }


    public void Frutas()
    {
            // Garante que a fruta seja gerada dentro dos limites da matriz
            float x = Random.Range(0, wallManager.instance.colunas) * wallManager.instance.tCelula;
            float y = Random.Range(0, wallManager.instance.linhas) * wallManager.instance.tCelula;

            Vector2 randomposition = new Vector2(x, y);

            // Verifica se a posi��o gerada est� dentro da matriz, arredondando para evitar posi��es fora
            //randomposition.x = Mathf.Clamp(randomposition.x, 0, (colunas - 1) * tCelula);
            //randomposition.y = Mathf.Clamp(randomposition.y, 0, (linhas - 1) * tCelula);

            // Instancia a fruta na posi��o gerada
            fruta = Instantiate(fruit, randomposition, Quaternion.identity);

            Debug.Log("Instanciou Fruta");
    }

    public void ZerarMapa()
    {
        for (int L = 0; L < linhas; L++) // Itera sobre as linhas
        {
            for (int A = 0; A < colunas; A++) // Itera sobre as colunas
            {
                /*if (mapa[L, A] != null)
                {
                    Destroy(mapa[L, A]); // Destroi o objeto na posi��o L, A
                }*/
            }
        }

        // Destroi a fruta
        if (fruta != null)
        {
            Destroy(fruta);
        }
    }

    
}
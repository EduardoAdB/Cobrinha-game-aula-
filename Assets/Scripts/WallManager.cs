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
    public void Linhas(string _linha)  //pega a linha no campo escrevivel
    {
        linhas = int.Parse(_linha); //define as linhas
    }

    public void Colunas(string _colunas) //pega a coluna no campo escrevivel
    {
        colunas = int.Parse(_colunas);  //define as colunas
    }


    public void GerarMapa()
    {
        Debug.Log("Linhas: " + linhas + ", Colunas: " + colunas);   
        mapa = new GameObject[linhas, colunas];  //define o mapa
        for (int L = 0; L < linhas; L++) // Itera sobre as linhas
        {
            for (int A = 0; A < colunas; A++) // Itera sobre as colunas
            {
                mapa[L,A] = Instantiate(wall, new Vector2(A * tCelula, L * tCelula), Quaternion.identity);  //instancia o mapa
            }
        }       
    }
    public void AjustarCamera()
    {
        // Calcula a maior dimensão do mapa para definir o tamanho da câmera
        float maiorDimensao = Mathf.Max(linhas, colunas); //calcula qual é o maior lado

        // Define uma margem extra para afastar a câmera
        float margem = 1.5f; // Ajuste este valor para mais ou menos afastamento

        // Ajusta o tamanho ortográfico da câmera para que ela cubra a maior dimensão + margem
        Camera.main.orthographicSize = (maiorDimensao * tCelula) / 2 + margem; //ajusta a câmera

        // Centraliza a câmera no centro do mapa
        Vector3 centroMatriz = new Vector3((colunas - 1) * tCelula / 2, (linhas - 1) * tCelula / 2, -dCamera);  //pega o centro da matriz e define a câmera
        Camera.main.transform.position = centroMatriz;  //ajusta a câmera
    }


    public void Frutas()
    {
            // Garante que a fruta seja gerada dentro dos limites da matriz
            float x = Random.Range(0, wallManager.instance.colunas) * wallManager.instance.tCelula;  //gera um x aleatório
            float y = Random.Range(0, wallManager.instance.linhas) * wallManager.instance.tCelula;   //gera um y aleatório

        Vector2 randomposition = new Vector2(x, y);

            // Verifica se a posição gerada está dentro da matriz, arredondando para evitar posições fora
            //randomposition.x = Mathf.Clamp(randomposition.x, 0, (colunas - 1) * tCelula);
            //randomposition.y = Mathf.Clamp(randomposition.y, 0, (linhas - 1) * tCelula);

            // Instancia a fruta na posição gerada
            fruta = Instantiate(fruit, randomposition, Quaternion.identity);  //pega uma posição aleatória para a fruta

            Debug.Log("Instanciou Fruta");
    }

    public void ZerarMapa()
    {
        for (int L = 0; L < linhas; L++) // Itera sobre as linhas
        {
            for (int A = 0; A < colunas; A++) // Itera sobre as colunas
            {
                if (mapa[L, A] != null) 
                {
                    //Destroy(wall); // Destroi o objeto na posição L, A  e não funciona
                }
            }
        }

        // Destroi a fruta
        if (fruta != null)  //checa se a fruta é diferente de nullo, se ela existe
        {
            Destroy(fruta); //destroi ela
        }
    }

    
}
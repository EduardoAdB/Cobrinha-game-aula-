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
        // Calcula a maior dimens�o do mapa para definir o tamanho da c�mera
        float maiorDimensao = Mathf.Max(linhas, colunas); //calcula qual � o maior lado

        // Define uma margem extra para afastar a c�mera
        float margem = 1.5f; // Ajuste este valor para mais ou menos afastamento

        // Ajusta o tamanho ortogr�fico da c�mera para que ela cubra a maior dimens�o + margem
        Camera.main.orthographicSize = (maiorDimensao * tCelula) / 2 + margem; //ajusta a c�mera

        // Centraliza a c�mera no centro do mapa
        Vector3 centroMatriz = new Vector3((colunas - 1) * tCelula / 2, (linhas - 1) * tCelula / 2, -dCamera);  //pega o centro da matriz e define a c�mera
        Camera.main.transform.position = centroMatriz;  //ajusta a c�mera
    }


    public void Frutas()
    {
            // Garante que a fruta seja gerada dentro dos limites da matriz
            float x = Random.Range(0, wallManager.instance.colunas) * wallManager.instance.tCelula;  //gera um x aleat�rio
            float y = Random.Range(0, wallManager.instance.linhas) * wallManager.instance.tCelula;   //gera um y aleat�rio

        Vector2 randomposition = new Vector2(x, y);

            // Verifica se a posi��o gerada est� dentro da matriz, arredondando para evitar posi��es fora
            //randomposition.x = Mathf.Clamp(randomposition.x, 0, (colunas - 1) * tCelula);
            //randomposition.y = Mathf.Clamp(randomposition.y, 0, (linhas - 1) * tCelula);

            // Instancia a fruta na posi��o gerada
            fruta = Instantiate(fruit, randomposition, Quaternion.identity);  //pega uma posi��o aleat�ria para a fruta

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
                    //Destroy(wall); // Destroi o objeto na posi��o L, A  e n�o funciona
                }
            }
        }

        // Destroi a fruta
        if (fruta != null)  //checa se a fruta � diferente de nullo, se ela existe
        {
            Destroy(fruta); //destroi ela
        }
    }

    
}
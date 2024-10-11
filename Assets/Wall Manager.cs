using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallManager : MonoBehaviour
{
    #region singleton
    public static wallManager instance;

    #endregion

    public GameObject wall;
    public GameObject fruit;



    public GameObject fruta;
    public int quantidadeFrutas;

    public int[,] mapa;
    public int linhas = 10;
    public int colunas = 15;
    public float tCelula = 1.1f;
    float dCamera = 10f;

    private void Awake()
    {
        mapa = new int[linhas, colunas];
        instance = this;
    }
    private void Start()
    {
        GerarMapa();
        Frutas();
    }

    void GerarMapa()
    {
        for (int A = 0; A < colunas; A++)
        {
            for (int L = 0; L < linhas; L++)
            {
                Instantiate(wall, new Vector2(L * tCelula, A * tCelula), Quaternion.identity);

            }
        }
        Vector3 centroMatriz = new Vector3((linhas - 1) * tCelula / 2, (colunas - 1) * tCelula / 2, -dCamera);
        Camera.main.transform.position = centroMatriz;
        Camera.main.orthographicSize = Mathf.Max(linhas, colunas) * tCelula / 2;
    }

    public void Frutas()
    {
        float x = Random.Range(0, linhas) * tCelula;
        float y = Random.Range(0, colunas) * tCelula;
        Vector2 randomposition = new Vector2(x, y);
        fruta = Instantiate(fruit, randomposition, Quaternion.identity);
    }

}
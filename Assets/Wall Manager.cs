using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public GameObject wall;

    int[,] mapa;
    public int linhas = 10;
    public int colunas = 15;
    public float tCelula = 1.1f;
    float dCamera = 10f;

    private void Awake()
    {
        mapa = new int[linhas, colunas];
    }
    private void Start()
    {
        GerarMapa();
        
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
} 

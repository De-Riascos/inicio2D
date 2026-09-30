using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int cantMonedas = 0;

    public bool atacando = false;

    public Text txtmoneda, txtPersonaje;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        txtmoneda.text = cantMonedas.ToString();
        txtPersonaje.text = "";
    }

    public void SetMonedas()
    {
        cantMonedas++;
        txtmoneda.text = cantMonedas.ToString();
        txtPersonaje.text = "He cogido " + cantMonedas + " monedas.";
    }
}

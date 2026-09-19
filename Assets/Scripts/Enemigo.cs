using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemigo : MonoBehaviour
{
    [Header("Salud")]  //serialize field permite el inspector cuando las variables son privadas y el header es como un encabezadp para organización visual
    public float salud;

    [Header("Disparo")]
    public Transform firePoint;
    public GameObject bala;
    public int distanciaPlayer;
    public float fireRate;

    [Header("Rotación")]
    public bool mirandoEnemigo;

    private GameObject target;

    [SerializeField] private float tiempoDisparo;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        float distancia = Vector2.Distance(transform.position, target.transform.position);
        Debug.Log(distancia);

        if (distancia < distanciaPlayer)
        {
            tiempoDisparo += Time.deltaTime;

            if (tiempoDisparo > fireRate)
            {
                tiempoDisparo = 0;
                Disparar();
            }

            MirarJugador();

        }
    }


    private void Disparar()
    {
        GameObject balin = Instantiate(bala, firePoint.position, firePoint.rotation);
        Destroy(balin, 3f);
    }


    void MirarJugador()
    {
        if (mirandoEnemigo && target.transform.position.x > transform.position.x
            || !mirandoEnemigo && target.transform.position.x < transform.position.x)
        {
            mirandoEnemigo = !mirandoEnemigo;

            transform.Rotate(0f, 180f, 0f);
        }
    }

    public void getDamage(float dmg)
    {

        salud -= dmg;

        if (salud <= 0)
        {
            Destroy(gameObject);
        }
    }
}

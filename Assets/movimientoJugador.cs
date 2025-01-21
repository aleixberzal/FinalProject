using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movimientoJugador : MonoBehaviour
{
    /*Referencia al rigibody 2D para poder mover el personaje a través de él*/

    private Rigidbody2D rb2D;
    //Header para ordenar el código
    [Header("Movimiento")]

    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadDeMovimiento;
    //Range para tener una barra en vez de un input de números para controlar el suavizado
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;
    //Ponemos la velocidad a zero porque no queremos que se mueva en el eje Z.
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    private void Start()
    {
        //Con el GetComponent le decimos que la variable tome el RB que tiene nuestro personaje.

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Toma la dirección a la que vamos y la velocidad.

        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
    }
    private void FixedUpdate()
    {
        //Se usa para cambios en las físicas para adaptarse a todos los dispositivos con diferentes FPS
        Mover(movimientoHorizontal * Time.fixedDeltaTime);
    }
    private void Mover(float mover)
    {
        //En el vector le decimos que no altere la velocidad cuando saltemos o caigamos
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        //Nos da un suavizado a la hora de acelerar o frenar con nuestro personaje
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
        if (mover > 0 && !mirandoDerecha)
        {
            //Girar
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            //Girar
            Girar();
        }
    }
    private void Girar()
    {
        //Nos pone mirando a la dirección que estamos viendo en caso de cambiar de dirección con un bool. Y multiplicamos por -1 para realizarlo. 
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}

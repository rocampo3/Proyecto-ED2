using UnityEngine;
using System.Collections;

public class ControladorPersonaje : MonoBehaviour {

	public float Salto = 100f;
	public bool Suelo = true;
	public Transform comprobar;
	private float comprobarRadio = 0.03f;
	public LayerMask mascara;
	public float speed = 5.0f;
	public float rotacion = 180f;
	private Animator animator;

	void Start () {
		
	}

	//encargado de orientar cada animacion a la debida accion
	void Awake() {
		animator = GetComponent<Animator> ();
	}

	//evita el doble salto, y manda las condiciones para la debida animacion.
	void FixedUpdate(){
		Suelo = Physics2D.OverlapCircle (comprobar.position, comprobarRadio, mascara);
		animator.SetBool ("isGrounded", Suelo);
		animator.SetFloat ("VelX", Mathf.Abs (Input.GetAxis ("Horizontal")));
	}

	//Controles de movimiento del personaje, spacio para saltar, A para moverse a la izquierda
	//y D para moverse a la derecha.
	void Update () {
		if (Suelo && Input.GetKey ("space")) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Salto);
			GetComponent<Rigidbody2D>().AddForce(new Vector2 (0, Salto));
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed,GetComponent<Rigidbody2D>().velocity.y);
			transform.localScale = new Vector3(1, 1, 1);
		}
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speed,GetComponent<Rigidbody2D>().velocity.y);
			transform.localScale = new Vector3(-1, 1, 1);
		}
	}


}

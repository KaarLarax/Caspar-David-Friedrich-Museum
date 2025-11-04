using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class SC_FP_Shooter7 : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public Transform vrCamera;
    Rigidbody rb;
    public float AlturaPersonaje = 2f;
    public Text TextVidas;
    public Text TextPociones;
    public Text TextMonedas;
    public Text TextMuertes;
    public Text TextMasterKey;
    public Text TextStatusBar;
    public Text TextGameOver;
    public Text TextPositionX;
    public Text TextPositionY;
    public Text TextPositionZ;
    public Text TextOrientacion;
    public Text TextAzimut;
    public Text TextAltitud;
    public Text NombreUsuario;
    public Text TextScore;
    int contadorVidas = 3;
    int contadorMonedas = 0;
    int contadorPociones = 0;
    int contadorMuertes = 0;
    int contadorScore = 0;
    public GameObject avatar;
    public float Xposition = 0f;
    public float Yposition = 0.0f;
    public float Zposition = 0f;
    public string sceneName;
    public GameObject balaPrefab;
    public Transform lanzador;
    public float VelDisparo;
    public float tiempoDisparo;
    private float inicioDisparar;
    public Text TextArmo;
    public Text TextArsenal;
    private int countBullet = 0;
    public int Arsenal = 100;
    int contadorArmo;
    public float lifetime = 5.0f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;
    static Animator anim;
    public GameObject itemIconPrefab;
    public Transform inventoryContent;
    private List<GameObject> uiInventory = new List<GameObject>();
    private List<Item> inventory = new List<Item>();

    public static SC_FP_Shooter7 instancia; // Referencia estática
    public Text textoObjetosDestruidos;

    void Awake()
    {
        instancia = this; // Guarda la referencia
    }
    public void AddToInventory(Item item)
    {
        inventory.Add(item);
        GameObject go = Instantiate(itemIconPrefab, inventoryContent);
        Image im = go.GetComponent<Image>();
        im.sprite = item.itemIcon;
        uiInventory.Add(go);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TeleportTrigger")
        {
            SceneManager.LoadScene(sceneName);
        }

        if (other.tag == "Teletrans")
        {
            avatar.transform.position = new Vector3(Xposition, Yposition, Zposition);
        }

        if (other.tag == "PortalPrincipal")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "ItemVidas")
        {
            Destroy(other.gameObject);
            contadorVidas++;
            if (contadorVidas > 0 && contadorMuertes > 0)
            {
                contadorMuertes--;
            }
            TextVidas.text = contadorVidas.ToString();
            TextMuertes.text = contadorMuertes.ToString();
            contadorScore += 10;
            TextScore.text = contadorScore.ToString();
        }
        if (other.tag == "ItemMuertes")
        {
            Destroy(other.gameObject);
            contadorMuertes++;
            if (contadorMuertes > 0 && contadorVidas != 0)
            {
                contadorVidas--;
            }
            TextMuertes.text = contadorMuertes.ToString();
            TextVidas.text = contadorVidas.ToString();
             contadorScore -= 50;
            TextScore.text = contadorScore.ToString();
        }

        if (other.tag == "ItemPociones")
        {
            Destroy(other.gameObject);
            contadorPociones++;
            TextPociones.text = contadorPociones.ToString();
            contadorScore += 100;
            TextScore.text = contadorScore.ToString();
        }

        if (other.tag == "ItemMonedas")
        {
            Destroy(other.gameObject);
            contadorMonedas++;
            TextMonedas.text = contadorMonedas.ToString();
            contadorScore += 10;
            TextScore.text = contadorScore.ToString();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ItemObstaculo"))
        {
            // Incrementar el contador de objetos destruidos
            IncrementarObjetosDestruidos();

            // Destruir el objeto y el proyectil
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }

    void Update()
    {
        // Movimiento del personaje
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Salto
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            anim.SetTrigger("isJumping");
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Aplicar gravedad
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Movimiento del personaje
        characterController.Move(moveDirection * Time.deltaTime);

        // Rotación del jugador y la cámara
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            textUpdate2();
        }

        // Disparo
        if (Input.GetButton("Fire2") && Time.time > inicioDisparar && Arsenal > 0)
        {
            instance();
            textUpdate();
        }

        AnimacionPersonaje();
    }

    private void textUpdate()
    {
        contadorArmo++;
        Arsenal--;
        //contadorObjetosDestruidos++;
        TextArmo.text = contadorArmo.ToString();
        TextArsenal.text = Arsenal.ToString();
    }

    private void textUpdate2()
    {
    if (TextPositionX != null) TextPositionX.text = Mathf.RoundToInt(transform.position.x).ToString();
    if (TextPositionY != null) TextPositionY.text = Mathf.RoundToInt(transform.position.y - 1f).ToString();
    if (TextPositionZ != null) TextPositionZ.text = Mathf.RoundToInt(transform.position.z).ToString();
    if (TextOrientacion != null) TextOrientacion.text = Mathf.RoundToInt(vrCamera.eulerAngles.y).ToString();
    if (TextAzimut != null) TextAzimut.text = Mathf.RoundToInt((vrCamera.eulerAngles.x - 360f) * -1f).ToString();
    if (TextAltitud != null) TextAltitud.text = Mathf.RoundToInt(transform.position.y - 1f).ToString();
    }

    private void instance()
    {
        inicioDisparar = Time.time + tiempoDisparo;
        GameObject balaPrefabInstanc;
        balaPrefabInstanc = Instantiate(balaPrefab, lanzador.position, Quaternion.identity);
        balaPrefabInstanc.GetComponent<Rigidbody>().AddForce(lanzador.forward * 100 * VelDisparo);
        balaPrefabInstanc.name = "Bala " + countBullet++;
        balaPrefabInstanc.AddComponent<Proyectil>(); // Agregar el script Proyectil al proyectil instanciado
        Destroy(balaPrefabInstanc, lifetime);
    }

    void AnimacionPersonaje()
    {
        // Control de animaciones del personaje basado en la entrada del jugador y el estado del personaje
        if (characterController.isGrounded)
        {
            // Si el personaje está en el suelo, verificar si está caminando, corriendo, atacando, realizando un combo o saltando
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                // Si el jugador está moviéndose, activar la animación de caminar
                anim.SetBool("isWalking", true);
            }
            else
            {
                // Si el jugador no está moviéndose, desactivar la animación de caminar
                anim.SetBool("isWalking", false);
            }

            // Verificar si el jugador está corriendo
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Si el jugador está corriendo, activar la animación de correr
                anim.SetBool("isRunning", true);
            }
            else
            {
                // Si el jugador no está corriendo, desactivar la animación de correr
                anim.SetBool("isRunning", false);
            }

            // Verificar si el jugador está saltando
            //if (!Input.GetButtonDown("Jump"))
            //{
                // Si el jugador no está saltando, desactivar la animación de salto
              //  anim.SetBool("isJumping", false);
            //}
        }
        else
        {
            // Si el personaje no está en el suelo, activar la animación de salto
            // Si el personaje no está en el suelo, activar la animación de salto
            //anim.SetTrigger("isJumping");
            // Desactivar otras animaciones que no correspondan mientras salta
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }


        // Verificar si el jugador está atacando
        if (Input.GetButtonDown("Fire1"))
        {
            // Si el jugador está atacando, activar la animación de ataque
            anim.SetTrigger("isAttack");
        }

        // Verificar si el jugador está realizando una patada
        if (Input.GetButtonDown("Fire1"))
        {
            // Si el jugador está realizando una patada, activar la animación de patada
            anim.SetTrigger("isKick");
        }

        // Verificar si el jugador está realizando un combo
        if (Input.GetButtonDown("Fire2"))
        {
            // Si el jugador está realizando un combo, activar la animación de combo
            anim.SetTrigger("isTrouwing");
        }

        // Verificar si el jugador está realizando un Sword Slash
        if (Input.GetButtonDown("Fire1"))
        {
            // Si el jugador está realizando un combo, activar la animación de combo
            anim.SetTrigger("isSlash");
        }
    }
     public void IncrementarObjetosDestruidos()
    {
        // Actualiza el Score y muestra en UI
        contadorScore += 50; // Suma 50 puntos por objeto destruido

        if (textoObjetosDestruidos != null)
            textoObjetosDestruidos.text = Proyectil.objetosDestruidos.ToString();

        if (TextScore != null)
            TextScore.text = contadorScore.ToString();
    }
}

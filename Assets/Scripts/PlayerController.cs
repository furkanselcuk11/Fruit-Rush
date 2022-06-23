using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [Header("Player Controller")]
    [SerializeField] private float speed = 5f;    // Player hareket h�z�
    [SerializeField] private float horizontalspeed = 5f; // Player y�n hareket h�z�
    [SerializeField] private float defaultSwipe = 4f;    // Player default kayd�rma mesafesi


    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (GameManager.gamemanagerInstance.startTheGame & !GameManager.gamemanagerInstance.isFinish)
        {
            // E�er StartGame true ve isFinish false ise hareket et
            transform.Translate(0, 0, speed * Time.fixedDeltaTime); // Karakter speed de�eri h�z�dna ileri hareket eder
            GameManager.gamemanagerInstance.CollectionBox.SetActive(true);
            anim.SetBool("isRunning", true);    // Ko�ma animasyonu �al���r
            MoveInput();    // Player hareket kontrol� �al��t�r
        }
        else
        {
            // E�er StartGame False ise  hareket etmez
            GameManager.gamemanagerInstance.CollectionBox.SetActive(false);
            anim.SetBool("isRunning", false);   // Ko�ma animasyonu durur ve default olarak bekleme animsayonu �al���r
        }
    }
    void MoveInput()
    {
        #region Mobile Controller 4 Direction

        float moveX = transform.position.x; // Player objesinin x pozisyonun de?erini al?r      
        float moveZ = transform.position.z; // Player objesinin z pozisyonun de?erini al?r           

        if (Input.GetKey(KeyCode.LeftArrow) || MobileInput.instance.swipeLeft)
        {   // E�er klavyede sol ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeLeft de�eri True ise  Sola hareket gider
            moveX = Mathf.Clamp(moveX - 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon s�n�rland�r�lmas� koyulacaksa
            // Player objesinin x (sol) pozisyonundaki gidece�i min-max s�n�r� belirler
        }
        else if (Input.GetKey(KeyCode.RightArrow) || MobileInput.instance.swipeRight)
        {   // E�er klavyede sa� ok tu�una bas�ld�ysa yada "MobileInput" scriptinin swipeRight de�eri True ise Sa�a hareket gider   
            moveX = Mathf.Clamp(moveX + 1 * horizontalspeed * Time.fixedDeltaTime, -defaultSwipe, defaultSwipe);    // Pozisyon s�n�rland�r�lmas� koyulacaksa
            // Player objesinin x (sa�) pozisyonundaki gidece�i min-max s�n�r� belirler
        }
        else
        {
            rb.velocity = Vector3.zero; // E?er hareket edilmediyse Player objesi sabit kals?n
        }

        transform.position = new Vector3(moveX, transform.position.y, moveZ);
        // Player objesinin pozisyonu moveX de�erine g�re x ekseninde, moveZ de�erine g�re z ekseninde hareket eder ve y ekseninde sabit kal�r  

        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fruit"))
        {
            GameManager.gamemanagerInstance.Add(other.gameObject);
        }
        if (other.CompareTag("Fail"))
        {
            if (GameManager.gamemanagerInstance.Collected.Count<2)
            {
                GameManager.gamemanagerInstance.Restart();
            }
        }
        if (other.CompareTag("Finish"))
        {
            // Finish alan�na geldiyse Level biter ve hareket etme false durumuna ge�er
            GameManager.gamemanagerInstance.isFinish = true;
            GameManager.gamemanagerInstance.startTheGame = false;
            // Dans animasyonu
            // Konfeti patlat            
        }
    }
}
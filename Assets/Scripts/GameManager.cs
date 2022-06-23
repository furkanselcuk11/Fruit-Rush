using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;

    [HideInInspector] public bool startTheGame; // Oyun ba?lad?m?
    [HideInInspector] public bool isFinish; // Level bittimi
    [Space]
    [Header("Game Controller")]
    [SerializeField] private GameObject Player;
    public GameObject CollectionBox;
    [SerializeField] private float swipeSpeed, diffBetweenItems;    // Toplana objelerin yana kayma h?z? ve objeler aras? mesafesi
    [Space]
    [Header("Collected Controller")]
    public List<Transform> Collected = new List<Transform>();   // Toplanan objelerin listesi
    //[Space]
    //[Header("Score Controller")]
    //public TextMeshProUGUI totalMoneyTxt;
    //public TextMeshProUGUI moneyTxt;
    //public int totalMoney;
    //private int money;

    private void Awake()
    {
        if (gamemanagerInstance == null)
        {
            gamemanagerInstance = this;
        }
    }
    void Start()
    {
        Collected.Add(CollectionBox.transform);    // Player objesini Toplanan Objeler listesine ekler
        //money = 0;
        //moneyTxt.text = money.ToString();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Collected.Count > 1)
        {
            for (int i = 1; i < Collected.Count; i++)
            {
                var firstItem = Collected.ElementAt(i - 1);
                var sectItem = Collected.ElementAt(i);

                // Stack (Toplama) i?lemi sonras? toplanan objelerin  s?ral? ?ekilde gidi?ini ayarlar
                sectItem.position = new Vector3(Mathf.Lerp(sectItem.position.x, firstItem.position.x, swipeSpeed * Time.fixedDeltaTime),
                    Mathf.Lerp(sectItem.position.y, firstItem.position.y + diffBetweenItems, swipeSpeed * Time.fixedDeltaTime),
                    firstItem.position.z);
            }
        }
    }

    public void Add(GameObject collectedObject)
    {
        // Stack (Toplama) i?lemi yapar
        collectedObject.transform.parent = null;
        collectedObject.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        collectedObject.gameObject.AddComponent<Stack>(); // Toplanan objeler Stack Componenti eklernir
        collectedObject.gameObject.GetComponent<Collider>().isTrigger = true; // Toplanan objelerin isTrigger aktif eder(Di?er objeler temas edince toplama yapmas? i?in)
        collectedObject.tag = "Collected";
        Collected.Add(collectedObject.transform); // Toplanan objeleri Collected listesine ekler
        AudioController.audioControllerInstance.Play("MoneySound");
    }
    public void Fail(GameObject failGate)
    {
        // Toplanan objeler silinir
        if (Collected.Count > 0)
        {
            int totalCollect = Collected.Count;
            for (int i = 0; i < totalCollect - 1; i++)
            {
                // totalCollect-1 olmas? player objesinin i?inde olmas?ndan ve silmemesi i?in
                Destroy(Collected.ElementAt(Collected.Count - 1).gameObject);   // T?m objeler silinir 
                Collected.RemoveAt(Collected.Count - 1); // Silinnen objeler Collected listesinden at?l?r               
            }
        }
        failGate.GetComponent<Collider>().enabled = false; // Fail(testere) kapýsýndan geçdiðimizde kapýnýn mesh collider kapat     
        AudioController.audioControllerInstance.Play("FailSound");
        // Eðer Engellere (Obstacles) çarpýldýysa karakter X ekseninde 0 noktasýna gelir
        startTheGame = false;   // Tekrar ekrana dokunan kadar hareket etmez
        Player.transform.position = new Vector3(Mathf.Lerp(0,Player.transform.position.x,Time.deltaTime),
            Player.transform.position.y, Player.transform.position.z);
    }
    public void Restart()
    {
        // E?er Toplanm?? obje yok ise ve Fail duvar?ndan ge?ilmi?se oyunu yeniden ba?lat
        startTheGame = false;   // Oyuna ba?lamak pasif olur
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}
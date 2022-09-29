using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanagerInstance;
    [SerializeField] private BasketSO basketType = null;    // Scriptable Objects eri�ir 
    [SerializeField] private MoneySO moneyType = null;    // Scriptable Objects eri�ir 

    public bool startGame; // Oyun ba?lad?m?
    [HideInInspector] public bool isFinish; // Level bittimi
    [Space]
    [Header("Game Controller")]
    [SerializeField] private GameObject Player;
    public GameObject CollectionBox;
    [SerializeField] private float swipeSpeed, diffBetweenItems;    // Toplana objelerin yana kayma h?z? ve objeler aras? mesafesi
    [Space]
    [Header("Collected Controller")]
    public List<Transform> Collected = new List<Transform>();   // Toplanan objelerin listesi

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
        basketType.currentFruit = basketType.minFruit;
        moneyType.currentMoney = moneyType.minMoney;
    }
    void Update()
    {
        UIController.uicontrollerInstance.UpdatePanel();
    }
    private void FixedUpdate()
    {
        if (Collected.Count > 1)
        {
            for (int i = 1; i < Collected.Count; i++)
            {
                var firstItem = Collected.ElementAt(i - 1);
                var sectItem = Collected.ElementAt(i);

                // Stack (Toplama) i�lemi sonras� toplanan objelerin  s�ral� �ekilde gidi�ini ve �st�ne eklemesini ayarlar
                sectItem.position = new Vector3(Mathf.Lerp(sectItem.position.x, firstItem.position.x, swipeSpeed * Time.fixedDeltaTime),
                    Mathf.Lerp(sectItem.position.y, firstItem.position.y + diffBetweenItems, swipeSpeed * Time.fixedDeltaTime),
                    firstItem.position.z);
            }
        }
    }
    public void StartTheGame()
    {
        startGame = true;
        isFinish = false;        
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
        basketType.currentFruit++;    // Toplanan meyve say�s�n� artt�r
        moneyType.currentMoney += basketType.fruitPrice;    // Oyun i�nde toplanan paray� artt�r
        UIController.uicontrollerInstance.UpdateUI();
    }
    public void Fail(GameObject failGate)
    {
        // Toplanan objeler silinir
        if (Collected.Count > 0)
        {
            int totalCollect = Collected.Count;
            for (int i = 0; i < totalCollect - 1; i++)
            {                
                // Toplanan objeler engellere temas ederse ortaya sa��l�r
                Collected.ElementAt(Collected.Count - 1).gameObject.transform.GetComponent<Rigidbody>().isKinematic=false;
                Collected.ElementAt(Collected.Count - 1).gameObject.transform.GetComponent<BoxCollider>().isTrigger=false;
                Collected.ElementAt(Collected.Count - 1).gameObject.transform.GetComponent<Rigidbody>().AddForce(transform.forward*500*Time.deltaTime,ForceMode.Impulse);
                // totalCollect-1 olmas? player objesinin i?inde olmas?ndan ve silmemesi i?in
                Destroy((Collected.ElementAt(Collected.Count - 1).gameObject),2);   // T�m objeler silinir 
                Collected.RemoveAt(Collected.Count - 1); // Silinnen objeler Collected listesinden at?l?r
                basketType.currentFruit--;    // Sepette toplanan meyve say�s�n� azalt
            }
        }
        failGate.GetComponent<BoxCollider>().isTrigger = true;
        failGate.GetComponent<Collider>().enabled = false; // Fail(testere) kap�s�ndan ge�di�imizde kap�n�n mesh collider kapat     
        AudioController.audioControllerInstance.Play("FailSound");
        moneyType.currentMoney = moneyType.minMoney;
        // E�er Engellere (Obstacles) �arp�ld�ysa karakter geri tepme yap�l�r
        startGame = false;   // Tekrar ekrana dokunan kadar hareket etmez
        Player.transform.position = Vector3.Lerp(Player.transform.position,
            new Vector3(0, Player.transform.position.y, Player.transform.position.z - 3f), swipeSpeed * Time.deltaTime);
    }
    public void Restart()
    {
        // E?er Toplanm?? obje yok ise ve Fail duvar?ndan ge?ilmi?se oyunu yeniden ba?lat
        startGame = false;   // Oyuna ba?lamak pasif olur
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }  
    public void NextLevelBuilding()
    {
        //SaveManager.saveManagerInstance.SaveGame(); // Verileri Kaydet
        SceneManager.LoadScene(1);
        basketType.totalFruit = basketType.currentFruit;    // Levelde toplanan meyve say�lar�n� toplam meyve say�s�na e�itler
        moneyType.totalMoney += moneyType.currentMoney;    // Levelde toplanan meyve say�lar�n� toplam meyve say�s�na e�itler
        moneyType.currentLevel++;   // E�er finish alan�na gelmi� ise bir sonraki leveli artt�r
    }
    public void NextLevel()
    {
        //SaveManager.saveManagerInstance.SaveGame(); // Verileri Kaydet
        //  Level bittikten sonra bir sonraki level ge�mek i�in butona bas�ld��� an �al��an fonksiyon
        SceneManager.LoadScene(RandomLevel());
    }
    public void GameExit()
    {
        //SaveManager.saveManagerInstance.SaveGame(); // Verileri Kaydet
        Application.Quit();
    }
    private int RandomLevel()
    {
        // Random level d�nderir
        int randomlevel =Random.Range(2,12); 
        return randomlevel;
    }
    public void FruitPriceLevel()
    {
        if (moneyType.totalMoney >= basketType.pricelevelUP)
        {
            moneyType.totalMoney -= basketType.pricelevelUP;
            basketType.fruitPrice +=10;            
            basketType.pricelevelUP = basketType.pricelevelUP * 2;
            basketType.priceLevel++;
            
            UIController.uicontrollerInstance.UpdateUI();
        }
        else
        {
            Debug.Log("Eksik para");
        }
    }
}
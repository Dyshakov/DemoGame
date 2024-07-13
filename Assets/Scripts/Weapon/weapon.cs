using System.Collections;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public GameObject muzzlePosition;
    public GameObject muzzleEffect;
    public GameObject bulletImpact;
    camera cameraComponent;
    public enemyController enemyController;
    public int bullets;
    private float lastFireTime;
    public float firRate;
    [SerializeField] float reloadTime = 3;
    public bool isReload;
    private int maxBullets = 30;
    private AudioSource audioSource;
    private AudioClip shotCLip;
    public GameObject bulletStore;
    private Animator bulletStoreAnimator;
    public float damage;
    private float recoil = 1;
    public Camera mainCamera;
    private bool isScope;
    GameObject player;
    Animator playerAnim;
    weaponAnimations weaponAnimations;
    PlayerScript playerScript;
    WeaponRecoil weaponRecoil;
    CamShake camShake;
    InventoryManager inventoryManager;
    InventoryItem activeInventoryItem;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] GameObject hitMarker;
       
    private Animator weaponAnimator;
    void Start()
    {
        hitMarker = GameObject.Find("hit_marker");
        hitMarker.GetComponent<Image>().enabled = false;
        if (!gameObject.CompareTag("enemyWeapon"))
        {
            cameraComponent = GameObject.Find("Main Camera").GetComponent<camera>();
            mainCamera = Camera.main;
        }
        weaponAnimations = GetComponent<weaponAnimations>();
        isScope = false;
        bulletStoreAnimator = bulletStore.GetComponent<Animator>();
        weaponAnimator = GetComponent<Animator>();
        lastFireTime = Time.time;
        isReload = false;
        audioSource = GetComponent<AudioSource>();
        shotCLip = audioSource.clip;
        player = GameObject.Find("Player");
        playerAnim = player.GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerScript>();
        weaponRecoil = GetComponent<WeaponRecoil>();
        camShake = GameObject.Find("Main Camera").GetComponent <CamShake>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > lastFireTime + 60 / firRate && !isReload && !gameObject.CompareTag("enemyWeapon") && !playerScript.isInventoryOpen)
        {
            Shot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReload && bullets != maxBullets && !gameObject.CompareTag("enemyWeapon") && !isScope)
        {
            Debug.Log(FindAmmoInInventory());
            // Если в инвентаре есть патроны
            if (inventoryManager.FindItem("ak47_bullets") != null)
            {
                playerScript.camWeapon.GetComponent<camWeaponScript>().Reload();
                playerScript.handsAnimation.Reload();
                audioSource.PlayOneShot(reloadSound);
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isReload && mainCamera)
        {
            Scope();
        }

        
    }

    public bool Shot()
    {
        if(!gameObject.CompareTag("enemyWeapon"))
        {
            activeInventoryItem = inventoryManager.inventorySlots[inventoryManager.selectedSlot].GetComponentInChildren<InventoryItem>();
            bullets = activeInventoryItem.count;

            if (bullets < 1)
            {
                return false;
            }
        }
        

        audioSource.PlayOneShot(shotCLip);
        lastFireTime = Time.time;
        //Если оружие не в руках бота
        if (!gameObject.CompareTag("enemyWeapon"))
        {
            weaponRecoil.Recoil();
            if (!isScope)
            {
                playerScript.handsAnimator.Play("handsShot");
                playerScript.camWeapon.GetComponent<Animator>().Play("weaponShot");
            }
            else
            {
                playerScript.camWeapon.GetComponent<Animator>().Play("ScopeShot");
            }
            playerScript.camWeapon.GetComponent<camWeaponScript>().ShotPlay();
            weaponAnimator.SetTrigger("shot");
            activeInventoryItem.count -= 1;
        }
        
        if (cameraComponent)
        {
            Instantiate(bulletImpact, cameraComponent.hit.point, new Quaternion(0, 0, 0, 0));
        }
        else if (enemyController)
        {
            Instantiate(bulletImpact, enemyController.hit.point, new Quaternion(0, 0, 0, 0));
        }
        
        

        // При попадании в игрока
        if (cameraComponent && cameraComponent.hit.collider && cameraComponent.hit.collider.GetComponent<health>() != null)
        {
            // Наносим урон
            cameraComponent.hit.collider.GetComponent<health>().healthpoints -= damage;
            audioSource.PlayOneShot(hitSound);
            StartCoroutine(Show_HitMarker());
        }
        // При попадании в бота
        else if (enemyController && enemyController.hit.collider && enemyController.hit.collider.GetComponent<PlayerHealth>() != null)
        {
            // Наносим урон игроку
            enemyController.hit.collider.GetComponent<PlayerHealth>().DamagePlayer(damage);
            
        }

        return true;
    }

    // Прицеливание

    private void Scope()
    {
        
        cameraComponent.Scope();
        if (!isScope)
        {
            playerScript.isScope = true;
            playerScript.handsAnimator.SetBool("isScope", true);
            mainCamera.fieldOfView = 30f;            // Зумим камеру
            isScope = true;                          
            playerAnim.SetBool("isScope", true);     // Проигрываем анимацию прицеливания для модельки игрока
        }
        else
        {
            playerScript.isScope = false;
            playerScript.handsAnimator.SetBool("isScope", false);
            playerAnim.SetBool("isScope", false);    // Выключаем анимацию для игрока
            isScope = false;
            mainCamera.fieldOfView = 60f;            // Возращаем обратно поле зрения камеры
        }
        
    }

    InventoryItem FindAmmoInInventory()
    {
        return inventoryManager.FindItem("ak47_bullets");
    }

    // Перезарядка
    IEnumerator Reload()
    {
        weaponAnimations.PlayReload();
        playerAnim.SetTrigger("reloadTrig");         // Проигрываем анимацию перезарядки для игрока
        isReload = true;
        yield return new WaitForSeconds(reloadTime); // Ждем времени перезарядки
        InventoryItem ammoInInventory = FindAmmoInInventory();  // патроны в инвентаре
        int bulletsNeed = maxBullets - activeInventoryItem.count;  // сколько патронов не хватает в магазине
        int bulletsToLoad = Mathf.Min(bulletsNeed, ammoInInventory.count); // сколько патронов можем загрузить
        if (bulletsNeed >= ammoInInventory.count)
        {
            Destroy(ammoInInventory.gameObject);
        }
        activeInventoryItem.count += bulletsToLoad;                       // Устанавливаем количество патронов в магазине
        ammoInInventory.count -= bulletsToLoad;
        isReload = false;
    }

    IEnumerator Show_HitMarker()
    {
        hitMarker.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(0.6f);
        hitMarker.GetComponent<Image>().enabled = false;
    }

    private void Awake()
    {

    }
}

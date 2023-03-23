using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPump : MonoBehaviour
{
    public static CharacterPump Instance { get; private set; }
    [Header("Character Stats")]
    [Space(20)]
    [SerializeField]
    public int damage = 10;
    [SerializeField]
    private float attackSpeed = 1.00f;
    [SerializeField]
    public int hp = 100;

    public int Damage { get { return damage; } }
    public float AttackSpeed { get { return attackSpeed; } }
    public int Hp { get { return hp; } }

    [Header("UI Elements")]
    [Space(20)]
    [SerializeField]
    private TextMeshProUGUI damageText;
    [SerializeField]
    private TextMeshProUGUI attackSpeedText;
    [SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField]
    private TextMeshProUGUI damageTextLevel;
    [SerializeField]
    private TextMeshProUGUI attackSpeedTextLevel;
    [SerializeField]
    private TextMeshProUGUI hpTextLevel;

    [SerializeField]
    private TextMeshProUGUI damagePriceText;
    [SerializeField]
    private TextMeshProUGUI attackSpeedPriceText;
    [SerializeField]
    private TextMeshProUGUI hpPriceText;

    [SerializeField]
    private Button pumpDamageButton;
    [SerializeField]
    private Button pumpAttackSpeedButton;
    [SerializeField]
    private Button pumpHpButton;

    [Header("Upgrade Prices")]
    [Space(20)]
    [SerializeField, Range(0, int.MaxValue)]
    private int damagePrice = 10;
    [SerializeField, Range(0, int.MaxValue)]
    private int attackSpeedPrice = 10;
    [SerializeField, Range(0, int.MaxValue)]
    private int hpPrice = 10;

    [Header("Upgrade Levels")]
    [Space(20)]
    [SerializeField]
    private int damageLevel = 0;
    [SerializeField]
    private int attackSpeedLevel = 0;
    [SerializeField]
    private int hpLevel = 0;

    [SerializeField]
    private CoinsManager coinsManager;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        damageText.text = "ATK: " + damage;
        attackSpeedText.text = "ASPD: " + attackSpeed.ToString("0.0");
        hpText.text = "HP: " + hp;

        damagePriceText.text = "Boost \n" + damagePrice;
        attackSpeedPriceText.text = "Boost \n" + attackSpeedPrice;
        hpPriceText.text = "Boost \n" + hpPrice;
    }

    public void BoostDamage()
    {
        if (coinsManager.Coins >= damagePrice && damagePrice >= 0)
        {
            coinsManager.Coins -= damagePrice;
            damageLevel++;
            damage += 10;
            damagePrice *= 2;
            damageTextLevel.text = "lvl: " + damageLevel;
        }
    }

    public void BoostAttackSpeed()
    {
        if (coinsManager.Coins >= attackSpeedPrice && attackSpeedPrice >= 0)
        {
            coinsManager.Coins -= attackSpeedPrice;
            attackSpeedLevel++;
            attackSpeed -= 0.1f;
            attackSpeedPrice *= 2;
            attackSpeedTextLevel.text = "lvl: " + attackSpeedLevel;
        }
    }

    public void BoostHp()
    {
        if (coinsManager.Coins >= hpPrice && hpPrice >= 0)
        {
            coinsManager.Coins -= hpPrice;
            hpLevel++;
            hp += 10;
            hpPrice *= 2;
            hpTextLevel.text = "lvl: " + hpLevel;
        }
    }
}
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Damagable : MonoBehaviourPun
{
    public int MaxHealth = 100;

    [SerializeField] private int currentHealth;
    private PhotonView view;

    public Canvas gameResultCanvas;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        Health = MaxHealth;
    }

    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            if (view.IsMine)
            {
                UpdateHealthUI((float)currentHealth / MaxHealth);
            }
        }
    }

    // public UnityEvent onDead;
    public void onDead()
    {
        PhotonNetwork.Destroy(transform.parent.gameObject);
        // TextMeshPro myPosition = GameObject.Find("Total player").GetComponent<TextMeshPro>();
        StaticScript.Instance.MyRank = StaticScript.Instance.IncreaseDeadCount() + 1;
        StaticScript.Instance.HideOrShowGameOverCanvas(true);
        StaticScript.Instance.isDead = true;
    }

    public void UpdateHealthUI(float currentPercentageHealth)
    {
        Slider slider = GameObject.Find("HealthBar").GetComponent<Slider>();
        GameObject.Find("UserID").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.AuthValues.UserId;
        slider.value = currentPercentageHealth;
    }

    public void Hit(int inflictedDamage)
    {
        view.RPC("HitRPC", RpcTarget.AllBuffered, inflictedDamage);
    }

    [PunRPC]
    void HitRPC(int inflictedDamage)
    {
        Health -= inflictedDamage;
        if (Health <= 0 && view.IsMine)
        {
            onDead();
        }
    }
}
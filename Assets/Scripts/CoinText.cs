using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coin;

    private void Awake()
    {
        _coin = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // CoinManager se ejecuta antes (DefaultExecutionOrder = -100)
        CoinManager.Instance.OnCoinAdd += ChangeUiText;
        ChangeUiText();
    }

    private void OnDisable()
    {
        if (CoinManager.Instance != null)
            CoinManager.Instance.OnCoinAdd -= ChangeUiText;
    }

    private void ChangeUiText()
    {
        _coin.text = "Coins: " + CoinManager.Instance.Amount;
    }
}

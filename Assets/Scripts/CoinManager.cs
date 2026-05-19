using System;
using UnityEngine;

[DefaultExecutionOrder(-100)] // se ejecuta antes que el resto
public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;
    public static CoinManager Instance { get { return _instance; } }

    private float _amount;
    public float Amount { get { return _amount; } }

    // Cualquier script puede subscribirse para que le avisen cuando cambien las monedas
    public Action OnCoinAdd;

    void Start()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public static void AddCoin(float amount)
    {
        if (_instance != null)
            _instance.AddCoinInternal(amount);
        else
            Debug.Log("Falta CoinManager en la scene");
    }

    private void AddCoinInternal(float amount)
    {
        _amount += amount;
        OnCoinAdd?.Invoke(); // avisa a quien escuche (la UI)
    }
}

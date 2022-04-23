using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float lifetime = 0.5f;
    public float distance = 2f;

    private Vector2 initialPosition;
    private Vector2 finalPosition;
    private float timer;

    public void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        initialPosition = transform.position;
        finalPosition = initialPosition + new Vector2(0f, distance);
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifetime - fraction));

        transform.position = Vector2.Lerp(initialPosition, finalPosition, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, Mathf.Sin(timer / lifetime));
    }

    public void SetText(int damage)
    {
        text.SetText(damage.ToString());
    }
}

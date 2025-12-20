using UnityEngine;
using PurrNet;

public class PlayerMovement : NetworkIdentity
{
    [SerializeField] private Color _color;
    [SerializeField] private Renderer _renderer;

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Changing Color");
            SetColor(_color);
        }

        Debug.Log(Input.GetAxis("Horizontal"));
        Debug.Log(Input.GetAxis("Vertical"));
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * (Time.deltaTime * 5f);
    }

    [ObserversRpc(bufferLast: true)]
    void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}

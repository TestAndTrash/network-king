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
            SetColor(_color);
        }
    }

    [ObserversRpc(bufferLast: true)]
    void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}

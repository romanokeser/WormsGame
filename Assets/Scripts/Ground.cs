using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Texture2D _baseTexture;

    private Texture2D _cloneTexture;
    private SpriteRenderer _spriteRenderer;

    private float _widthWorld, _heightWorld;
    private float _widthPixel, _heightPixel;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _cloneTexture = Instantiate(_baseTexture);

        _cloneTexture.alphaIsTransparency = true;

        UpdateTexture();

        gameObject.AddComponent<PolygonCollider2D>();
    }

    #region Getters

    public float WidthWorld
    {
        get
        {
            if (_widthWorld == 0)
                _widthWorld = _spriteRenderer.bounds.size.x;

            return _widthWorld;
        }
    }

    public float HeightWorld
    {
        get
        {
            if (_heightWorld == 0)
                _heightWorld = _spriteRenderer.bounds.size.y;

            return _heightWorld;
        }
    }


    public float WidthPixel
    {
        get
        {
            if (_widthPixel == 0)
                _widthPixel = _spriteRenderer.sprite.texture.height;

            return _widthPixel;
        }
    }

    public float HeightPixel
    {
        get
        {
            if (_heightPixel == 0)
                _heightPixel = _spriteRenderer.sprite.texture.height;

            return _heightPixel;
        }
    }


    #endregion

    private void UpdateTexture()
    {
        _spriteRenderer.sprite = Sprite.Create(_cloneTexture,
            new Rect(0, 0, _cloneTexture.width, _cloneTexture.height),
            new Vector2(0.5f, 0.5f), 50f);
    }
}

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

    Vector2Int World2Pixel(Vector2 pos)
    {
        Vector2Int v = Vector2Int.zero;
        float dx = (pos.x - transform.position.x);
        float dy = (pos.y - transform.position.y);

        v.x = Mathf.RoundToInt(0.5f * WidthPixel + dx * (WidthPixel / WidthWorld));
        v.y = Mathf.RoundToInt(0.5f * HeightPixel + dx * (HeightPixel / HeightWorld));

        return v;
    }

    private void MakeAHole(CircleCollider2D col)
    {
        Vector2Int c = World2Pixel(col.bounds.center);

        int r = Mathf.RoundToInt(col.bounds.size.x * WidthPixel / WidthWorld);
        int px, nx, py, ny, d;

        for (int i = 0; i <= r; i++)
        {
            d = Mathf.RoundToInt(Mathf.Sqrt(r * r - i * r));
            for (int j = 0; j <= d ; j++)
            {
                px = c.x + i;
                nx = c.x - i;
                py = c.y + j;
                ny = c.y - j;

                _cloneTexture.SetPixel(px, py, Color.clear);
                _cloneTexture.SetPixel(nx, py, Color.clear);
                _cloneTexture.SetPixel(px, ny, Color.clear);
                _cloneTexture.SetPixel(nx, ny, Color.clear);
            }
        }

        _cloneTexture.Apply();
        UpdateTexture();

        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>(); //setting collider as the same shape of the destroyed texture
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (!collision.CompareTag("bullet"))
        //    return;

        //if (!collision.GetComponent<CircleCollider2D>())
        //    return;

        //MakeAHole(collision.GetComponent<CircleCollider2D>());
        //Destroy(collision.gameObject, 0.1f);
    }

}

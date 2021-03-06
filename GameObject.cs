using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class GameObject
{
    // Fields
    protected bool _active = true;
    protected Vector2 _position = Vector2.Zero;
    protected Texture2D _texture;
    protected Rectangle _collider = Rectangle.Empty;
    private List<Texture2D> _textures = new List<Texture2D>();

    // Properties
    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }

    public Vector2 Position
    {
        get { return _position; }
        set
        {
            _position = value;
            _collider.X = (int)value.X;
            _collider.Y = (int)value.Y;
        }
    }

    public Texture2D Texture
    {
        get { return _texture; }
        set
        {
            _texture = value;
            _collider = new Rectangle((int)_position.X, (int)_position.Y, value.Width, value.Height);
        }
    }
    public List<Texture2D> Textures { get { return _textures; } }

    public Rectangle Collider//Readonly
    {
        get { return _collider; }
    }

    public int Width//Readonly
    {
        get { return _texture.Width; }
    }

    public int Height//Readonly
    {
        get { return _texture.Height; }
    }

    //Constructors
    public GameObject() { }

    // Copy constructor
    public GameObject(GameObject pOriginal)
    {
        _active = pOriginal._active;
        _position = pOriginal._position;
        _texture = pOriginal._texture;
        _collider = pOriginal._collider;
    }
    public GameObject(List<Texture2D> textureList)
    {
        _textures = textureList;
        Texture = _textures[0];
    }

    //Methods
    public bool Collision(GameObject pOther)
    {
        if (_active && _collider.Intersects(pOther._collider))
            return true;
        return false;
    }

    public bool Contains(Point pPoint)
    {
        if (_active & _collider.Contains(pPoint))
        {
            return true;
        }
        return false;
    }

    public void Draw(SpriteBatch pSpriteBatch)
    {
        if (Active)
        {
            pSpriteBatch.Draw(_texture, _position, Color.White);
        }
    }

    public void Draw(SpriteBatch pSpriteBatch, Color pColor, float pScale = 1)
    {
        if (Active)
        {
            Vector2 scale = Vector2.One * pScale;
            pSpriteBatch.Draw(_texture, _position, null, pColor, 0, Vector2.One / 2, scale, SpriteEffects.None, 0);
        }
    }
    public virtual void Load() { }
    public virtual void Update(GameTime pGameTime) { }
    public virtual void Update(GameTime pGameTime, Dictionary<string, GameObject> gameObjects) { }
    public virtual void Update(GameTime pGameTime, List<GameObject> gameObjects) { }
    public virtual void ChangeTexture(int i) { }
    public virtual void DrawButton(SpriteBatch spriteBatch) { }
    public virtual void ChangeState(string name) { }
}

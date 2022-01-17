using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
//eigen button!!
namespace DiscoFramework
{
    public class Button : GameObject
    {
        private string _Text = "";
        private SpriteFont _Font;
        private Color _TextColor = Color.Black;
        private MouseState lastState;

        private Color stateColor = Color.White;
        private buttonState state = buttonState.standard;

        public event EventHandler OnClick;
        //enum for the buttonstate, default is standard
        private enum buttonState
        {
            standard,
            hovered,
            clicked
        }

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public SpriteFont Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        public Color TextColor
        {
            get { return _TextColor; }
            //default black
            set { _TextColor = value; }
        }

        public Button() { }

        public Button(Button original)
        {
            _active = original._active;
            _texture = original._texture;
            _position = original._position;
            _collider = original._collider;
            _Text = original._Text;
            _Font = original._Font;
            _TextColor = original._TextColor;
        }

        public override void DrawButton(SpriteBatch pSpriteBatch)
        {
            //zorgt er voor dat de button in het midden wordt gezet van de positie die je aangeeft, en niet links bovenaan
            var pos = new Vector2(_position.X - (_texture.Width / 2), _position.Y - (_texture.Height / 2));
            _collider = new Rectangle((int)pos.X, (int)pos.Y, _texture.Width, _texture.Height);

            if (Active)
            {
                //draw button
                pSpriteBatch.Draw(_texture, pos, stateColor);
                //checkt of er tekst is meegeven zoja wordt het in het midden van de button gezet
                if (!string.IsNullOrEmpty(_Text))
                {
                    var x = (_collider.X + (_collider.Width / 2)) - (_Font.MeasureString(Text).X / 2);
                    var y = (_collider.Y + (_collider.Height / 2)) - (_Font.MeasureString(Text).Y / 2);
                    pSpriteBatch.DrawString(_Font, _Text, new Vector2(x, y), _TextColor);
                }
            }
        }

        private void CheckClick()
        {
            var mousePoint = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            if (Collider.Contains(mousePoint))
            {
                state = buttonState.hovered;
                if (OnClick != null && Mouse.GetState().LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released)
                {
                    state = buttonState.clicked;
                    OnClick.Invoke(this, EventArgs.Empty);
                }
                lastState = Mouse.GetState();
            }
            else
            {
                state = buttonState.standard;
            }
        }

        private void State()
        {
            switch (state)
            {
                case buttonState.standard:
                    stateColor = Color.White;
                    break;
                case buttonState.hovered:
                    stateColor = Color.Gray;
                    break;
                case buttonState.clicked:
                    stateColor = Color.Black;
                    stateColor = Color.White;
                    break;
            }
        }
        public override void Update(GameTime pGameTime)
        {
            State();
            CheckClick();
        }
        public override void Load()
        {

        }
    }
}


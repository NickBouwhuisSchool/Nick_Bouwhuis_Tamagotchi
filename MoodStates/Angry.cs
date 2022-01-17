using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MoodStates
{
    public class Angry : MoodStateBase
    {
        public Angry()
        {

        }
        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>("angry");
        }

        public override void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            base.Draw(pGameTime, batch);
        }
    }
}

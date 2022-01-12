using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MoodStates
{
    public class Dead : MoodStateBase
    {
        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>("dead");
        }

        public override void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            base.Draw(pGameTime, batch);
        }
    }
}

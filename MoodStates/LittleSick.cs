using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MoodStates
{
    public class LittleSick : MoodStateBase
    {
        public LittleSick()
        {

        }
        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>("LittleSick");
        }

        public override void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            base.Draw(pGameTime, batch);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MoodStates
{
    public class VerySick : MoodStateBase
    {
        public override void Load(ContentManager content)
        {
            base.Load(content);
            Texture = content.Load<Texture2D>("VerySick");
        }

        public override void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            base.Draw(pGameTime, batch);
        }
    }
}

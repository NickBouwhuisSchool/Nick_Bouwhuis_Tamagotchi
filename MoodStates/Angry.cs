using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MoodStates
{
    class Angry : MoodStateBase
    {
        private Texture2D texture;
        public override void Load(ContentManager content)
        {
            base.Load(content);
            texture = content.Load<Texture2D>("angry");
        }

        public override void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            base.Draw(pGameTime);
            batch.Draw(texture, new Vector2);
        }
    }
}

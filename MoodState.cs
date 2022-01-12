using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MoodStateBase
{
    protected Texture2D Texture { get; set; }
    public virtual void Load(ContentManager content)
    {

    }
    public virtual void Update(GameTime pGameTime)
    {

    }
    public virtual void Draw(GameTime pGameTime, SpriteBatch batch)
    {
        batch.Draw(Texture, new Vector2(128, 120), Color.White);
    }

}

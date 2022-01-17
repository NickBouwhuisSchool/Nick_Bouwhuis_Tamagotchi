using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

//basis van de moodstates, elke is alleen voor de texture omdat het makkelijker was in dit geval dat de statemanager alles checkte.
public class MoodStateBase
{
    
    protected Texture2D Texture { get; set; }
    public MoodStateBase()
    {

    }
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

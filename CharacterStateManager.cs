using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MoodStates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Nick_Bouwhuis_Tamagotchi
{
    class CharacterStateManager
    {
        private MoodStateBase currentState;
        private Angry angry = new Angry();
        private Dead dead = new Dead();
        private Excited excited = new Excited();
        private Happy happy = new Happy();
        private Hungry hungry = new Hungry();
        private LittleSick littleSick = new LittleSick();
        private Sad sad = new Sad();
        private Sleeping sleeping = new Sleeping();
        private VerySick verySick = new VerySick();

        private ContentManager _content;

        public void Initialize()
        {
            currentState = verySick;
        }
        public void Load(ContentManager content)
        {
            currentState.Load(content);
            _content = content;
        }
        public void Update(GameTime pGameTime)
        {
            currentState.Update(pGameTime);
        }
        public void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            currentState.Draw(pGameTime, batch);
        }
        public void ChangeState(string state)
        {
            switch (state)
            {
                case "Angry":
                    currentState = angry;
                    Load(_content);
                    break;
                case "Dead":
                    currentState = dead;
                    Load(_content);
                    break;
                case "Excited":
                    currentState = excited;
                    Load(_content);
                    break;
                case "Happy":
                    currentState = happy;
                    Load(_content);
                    break;
                case "Hungry":
                    currentState = hungry;
                    Load(_content);
                    break;
                case "LittleSick":
                    currentState = littleSick;
                    Load(_content);
                    break;
                case "Sad":
                    currentState = sad;
                    Load(_content);
                    break;
                case "Sleeping":
                    currentState = sleeping;
                    Load(_content);
                    break;
                case "VerySick":
                    currentState = verySick;
                    Load(_content);
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MoodStates;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Timers;

namespace Nick_Bouwhuis_Tamagotchi
{
    class CharacterStateManager
    {
        private MoodStateBase currentState;
        private Angry angry = new Angry();
        private Blink blink = new Blink();
        private Dead dead = new Dead();
        private Excited excited = new Excited();
        private Happy happy = new Happy();
        private Hungry hungry = new Hungry();
        private LittleSick littleSick = new LittleSick();
        private Sad sad = new Sad();
        private Sleeping sleeping = new Sleeping();
        private VerySick verySick = new VerySick();

        private ContentManager _content;

        private Random rand = new Random();
        private Timer t = new Timer();
        private SpriteFont font;
        private Timer inputTimer = new Timer();

        public int Attention { get; set; }
        public int Hunger { get; set; }
        public bool ButtonInput { get; set; }

        public void Initialize()
        {
            currentState = happy;
        }
        public void Load(ContentManager content)
        {
            currentState.Load(content);
            _content = content;
        }
        public void Update(GameTime pGameTime)
        {
            currentState.Update(pGameTime);
            Blink();
            if (ButtonInput)
                Input();
            Console.WriteLine(currentState.ToString());

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
                case "Blink":
                    currentState = blink;
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

        public void Blink()
        {
            if (currentState == happy)
            {
                int randomInterval = rand.Next(100);
                if (randomInterval > 97)
                {
                    currentState = blink;
                    Load(_content);
                    t.Interval = 500;
                    t.Enabled = true;
                    t.Elapsed += new ElapsedEventHandler(StopBlink);
                }
            }
        }
        public void StopBlink(object source, ElapsedEventArgs e)
        {
            currentState = happy;
        }

        public void Input()
        {
            Console.WriteLine(currentState.ToString());
            ButtonInput = false;
            ChangeState("Happy");
            inputTimer.Stop();
            inputTimer.Interval = 20000;
            inputTimer.Elapsed += new ElapsedEventHandler(Sleep);
            inputTimer.Enabled = true;
        }
        private void Sleep(Object o, ElapsedEventArgs e)
        {
            ChangeState("Sleeping");
            t.Stop();
        }
    }
}

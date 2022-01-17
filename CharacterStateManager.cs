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
        private MoodStateBase stateAfterSleep;
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
        Timer timer = new Timer();

        public int Attention { get; set; }
        public int Hunger { get; set; }
        public bool ButtonInput { get; set; }
        public bool Dead { get; set; }

        public void Initialize()
        {
            currentState = happy;
            stateAfterSleep = happy;
        }
        public void Load(ContentManager content)
        {
            _content = content;
            angry.Load(content);
            blink.Load(content);
            dead.Load(content);
            excited.Load(content);
            happy.Load(content);
            hungry.Load(content);
            littleSick.Load(content);
            sad.Load(content);
            sleeping.Load(content);
            verySick.Load(content);
        }
        public void Update(GameTime pGameTime)
        {
            currentState.Update(pGameTime);

            if (currentState == happy)
                Blink();
            if (ButtonInput)
                Input("");
            Console.WriteLine(stateAfterSleep.ToString());


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
                    break;
                case "Blink":
                    currentState = blink;
                    break;
                case "Dead":
                    currentState = dead;
                    break;
                case "Excited":
                    currentState = excited;
                    break;
                case "Happy":
                    currentState = happy;
                    break;
                case "Hungry":
                    currentState = hungry;
                    break;
                case "LittleSick":
                    currentState = littleSick;
                    break;
                case "Sad":
                    currentState = sad;
                    break;
                case "Sleeping":
                    currentState = sleeping;
                    break;
                case "VerySick":
                    currentState = verySick;
                    break;
            }
        }

        public void Blink()
        {
            if (!Dead)
            {
                int randomInterval = rand.Next(100);
                if (randomInterval > 97)
                {
                    currentState = blink;
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

        public void Input(string buttonPressed)
        {
            ButtonInput = false;
            if(currentState == sleeping)
            {
                if (Hunger < 50)
                {
                    HungerState();
                }
                else
                    AttentionState();
                currentState = stateAfterSleep;
            }
            inputTimer.Stop();
            inputTimer.Interval = 20000;
            inputTimer.Elapsed += new ElapsedEventHandler(Sleep);
            inputTimer.Enabled = true;
            if(buttonPressed == "Left")
            {
                if (Attention < 100)
                    Attention += 10;
            }
            else if(buttonPressed == "Right")
            {
                if (Hunger < 100 && currentState == angry)
                {

                }
                else
                    Hunger += 10;
            }
        }
        private void Sleep(Object o, ElapsedEventArgs e)
        {
            if (!Dead)
            {
                t.Stop();
                t.Dispose();
                ChangeState("Happy");
                ChangeState("Sleeping");
            }
        }

        public void DecreaseTimer()
        {
            timer.Stop();
            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler(DecreaseTimerElapsed);
            timer.Enabled = true;
        }
        private void DecreaseTimerElapsed(object o, ElapsedEventArgs e)
        {
            Hunger -= 5;
            Attention -= 0;
            if(Hunger < 50)
            {
                HungerState();
            }
            else
            AttentionState();
        }
        private void AttentionState()
        {
            if (currentState == sleeping)
            {
                if (Attention > 50)
                {
                    stateAfterSleep = happy;
                    happy.Load(_content);
                }
                else if (Attention < 40 && Attention > 20)
                {
                    stateAfterSleep = sad;
                    sad.Load(_content);
                }
                else if (Attention < 20)
                {
                    stateAfterSleep = angry;
                    angry.Load(_content);
                }
            }
            else
            {
                if (Attention > 50)
                {
                    ChangeState("Happy");
                }
                else if (Attention < 40 && Attention > 20)
                {
                    ChangeState("Sad");
                }
                else if (Attention < 20)
                {
                    ChangeState("Angry");
                }
            }
        }
        private void HungerState()
        {
            if (Hunger < 50 && Hunger >= 40)
            {
                ChangeState("Hungry");
            }
            else if(Hunger < 40 && Hunger >= 30)
            {
                ChangeState("LittleSick");
            }
            else if(Hunger < 30 && Hunger >= 6)
            {
                ChangeState("VerySick");
            }
            else if(Hunger <= 5)
            {
                Died();
            }
        }
        private void Died()
        {
            currentState = dead;
            Dead = true;
        }
    }
}

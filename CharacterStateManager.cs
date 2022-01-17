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
        private bool excitedActive = false;
        //property om te checken of de tamagochi excited is
        public bool ExcitedActive {
            get { return excitedActive; }
            set { excitedActive = value; }
        }
        //property hoe veel de attention span is
        public int Attention { get; set; }
        //property voor de hunger
        public int Hunger { get; set; }
        //property om te kijken of er een button is ingeklikt
        public bool ButtonInput { get; set; }
        //property om te kijken of de tamagotchi dood is
        public bool Dead { get; set; }
        public CharacterStateManager()
        {

        }

        public void Initialize()
        {
            currentState = happy;
            stateAfterSleep = happy;
        }
        public void Load(ContentManager content)
        {
            //alle states laden
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
            //checken of de tamagochi blij is, zoja gaat hij blinken
            if (currentState == happy)
                Blink();
            //check of er een knop ingedrukt is
            if (ButtonInput)
                Input("");
            //als de hunger hoger dan 120 is
            if(Hunger >= 120)
            {
                Excited();
                excitedActive = true;
            }
            //als hij slaapt en niet excited is niks nieuws showen
            if(currentState == sleeping && !excitedActive)
            {

            }
            else
            {
                //als de hunger hoger is dan 50 en niet excited is de standaard attention state doen, zo kan hij iets anders uitvoert als deze criteria niet zo is
                if(Hunger >= 50 && !excitedActive)
                {
                    AttentionState();
                }
                //als de honger lager dan 50 is de hungerstates checken
                else if(Hunger <= 50)
                {
                    HungerState();
                }
            }


        }
        public void Draw(GameTime pGameTime, SpriteBatch batch)
        {
            //de current state drawen
            currentState.Draw(pGameTime, batch);
        }
        //deze method kan ik de states makkelijker veranderen
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
        //blink method, random intervals dat hij knippert voor een halve seconde
        public void Blink()
        {
            if (!Dead && !excitedActive && Hunger >= 50)
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
        //als de timer af gaat stopt hij met blinken
        public void StopBlink(object source, ElapsedEventArgs e)
        {
            currentState = happy;
        }
        //als er een input is voert hij deze code uit
        public void Input(string buttonPressed)
        {
            ButtonInput = false;
            //check of de state sleeping is
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
            //timer die na 20 seconde af gaat en dan ervoor zorgt dat de tamagochi gaat slapen
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
                if (Hunger < 100 && currentState == angry || stateAfterSleep == angry)
                {

                }
                else
                    Hunger += 10;
            }
        }
        //method die wordt aangeroepen als de timer af gaat
        private void Sleep(Object o, ElapsedEventArgs e)
        {
            if (!Dead && !excitedActive && Hunger >= 50)
            {
                t.Stop();
                ChangeState("Happy");
                ChangeState("Sleeping");
            }
        }
        //timer die constant aangeroepen wordt om de 5 seconde zodat er hunger en attention af gaat
        public void DecreaseTimer()
        {
            timer.Stop();
            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler(DecreaseTimerElapsed);
            timer.Enabled = true;
        }
        //code die wordt uitgevoerd als de timer af gaat
        private void DecreaseTimerElapsed(object o, ElapsedEventArgs e)
        {
            Hunger -= 5;
            Attention -= 5;
            if(Hunger < 50)
            {
                HungerState();
            }
            else
            AttentionState();
        }
        //kijken welke attentionstate hij moet uitvoeren
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
        //kijken welke hungerstate er uitgevoerd moet worden
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
        //kan worden aangeroepen worden als de tamagochi dood moet gaan
        private void Died()
        {
            currentState = dead;
            Dead = true;
        }
        //wordt uitgevoerd als de hunger boven de 120 is
        private void Excited()
        {
            ChangeState("Excited");
            Timer t = new Timer();
            //timer die af gaat na 30 seconde
            t.Interval = 30000;
            t.Elapsed += new ElapsedEventHandler(ExcitedTimerElapsed);
            t.Enabled = true;
            Console.WriteLine("test1");
        }
        private void ExcitedTimerElapsed(Object o, ElapsedEventArgs e)
        {
            Timer t = new Timer();
            //timer die af gaat na 10 seconde en dan gaat de tamagochi dood
            t.Interval = 10000;
            t.Elapsed += ExcitedDead;
            t.Enabled = true;
            Console.WriteLine("test2");
        }
        //wordt uitgevoerd door de timer hierboven
        private void ExcitedDead(Object o, ElapsedEventArgs e)
        {
            Died();
        }
        //reset excited als er een goede button combination wordt uitgevoerd
        public void ExcitedReset()
        {
            Hunger = 110;
            excitedActive = false;
        }
    }
}
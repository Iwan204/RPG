using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ruminate.GUI.Content;
using Ruminate.GUI.Framework;
using Microsoft.Xna.Framework.Content;

namespace RPG_V0._3
{
    public abstract class Screen
    {
        public Color Color { get; set; }

        public abstract void Init(Game1 game);
        public abstract void OnResize();
        public abstract void Update(GameTime time);
        public abstract void Draw();
    }

    class CharCreateScreen : Screen
    {
        Gui gui;
        Panel CharCreatePanel;
        Panel AttributePanel;
        Panel[] IndAttributePanels;
        Skin skin;
        Text text;
        Color AttributeColour;

        //Attribute Variables
        AttributesStruct Attributes;

        bool resizeNeeded;

        Label LabelAGEVAL;
        Label LabelINTVAL;
        Label LabelSTRVAL;
        Label LabelDEXVAL;
        Label LabelCONVAL;
        Label LabelCHAVAL;

        public override void Draw()
        {
            gui.Draw();
        }

        public override void Init(Game1 game)
        {
            Color = Color.White;
            skin = new Skin(game.GreyImageMap, game.GreyMap);
            text = new Text(game.GreySpriteFont, Color.Chartreuse);
            AttributeColour = Color.OrangeRed;
            resizeNeeded = false;

            Attributes = new AttributesStruct();
            Attributes.Age = 16;

            gui = new Gui(game, skin, text);
            gui.AddText("Nixie", new Text(game.Nixie, AttributeColour));

            CharCreatePanel = new Panel(1, 1, game.GraphicsDevice.Viewport.Width / 3, game.GraphicsDevice.Viewport.Height - 10);
            CharCreatePanel.AddWidget(new Label(1 ,6,"NAME"));
            SingleLineTextBox NameBox = new SingleLineTextBox(50, 0, 150, 1);
            NameBox.Active = true;
            CharCreatePanel.AddWidget(NameBox);

            AttributePanel = new Panel(1,50,CharCreatePanel.Area.Width -10,70);
            IndAttributePanels = new Panel[6];

            //strength
            Panel PanelSTR = new Panel(1, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            LabelSTRVAL = new Label(1, 1, Convert.ToString(Attributes.Strength));
            LabelSTRVAL.Text = "Nixie";
            Label LabelSTR = new Label(1, 35, "STR");
            PanelSTR.AddWidget(LabelSTR);
            PanelSTR.AddWidget(LabelSTRVAL);
            IndAttributePanels[0] = PanelSTR;

            //Dex
            Panel PanelDEX = new Panel((AttributePanel.Area.Width / 6) * 1, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            Label LabelDEX = new Label(1, 35, "DEX");
            LabelDEXVAL = new Label(1, 1, Convert.ToString(Attributes.Dexterity));
            LabelDEXVAL.Text = "Nixie";
            PanelDEX.AddWidget(LabelDEX);
            PanelDEX.AddWidget(LabelDEXVAL);
            IndAttributePanels[1] = PanelDEX;

            //Charisma
            Panel PanelCHA = new Panel((AttributePanel.Area.Width / 6) * 2, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            Label LabelCHA = new Label(1, 35, "CHA");
            LabelCHAVAL = new Label(1, 1, Convert.ToString(Attributes.Charisma));
            LabelCHAVAL.Text = "Nixie";
            PanelCHA.AddWidget(LabelCHA);
            PanelCHA.AddWidget(LabelCHAVAL);
            IndAttributePanels[2] = PanelCHA;

            //Intelligence
            Panel PanelINT = new Panel((AttributePanel.Area.Width / 6) * 3, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            Label LabelINT = new Label(1, 35, "INT");
            LabelINTVAL = new Label(1, 1, Convert.ToString(Attributes.Intelligence));
            LabelINTVAL.Text = "Nixie";
            PanelINT.AddWidget(LabelINT);
            PanelINT.AddWidget(LabelINTVAL);
            IndAttributePanels[3] = PanelINT;

            //Constitution
            Panel PanelCON = new Panel((AttributePanel.Area.Width / 6) * 4, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            Label LabelCON = new Label(1, 35, "CON");
            LabelCONVAL = new Label(1, 1, Convert.ToString(Attributes.Constitution));
            LabelCONVAL.Text = "Nixie";
            PanelCON.AddWidget(LabelCON);
            PanelCON.AddWidget(LabelCONVAL);
            IndAttributePanels[4] = PanelCON;

            //Age
            Panel PanelAGE = new Panel(((AttributePanel.Area.Width / 6) * 5 )-5, 1, AttributePanel.Area.Width / 6, AttributePanel.Area.Height - 10);
            Label LabelAGE = new Label(1, 35, "AGE");
            LabelAGEVAL = new Label(1, 1, Convert.ToString(Attributes.Age));
            LabelAGEVAL.Text = "Nixie";

            PanelAGE.AddWidget(LabelAGE);
            PanelAGE.AddWidget(LabelAGEVAL);
            IndAttributePanels[5] = PanelAGE;

            AttributePanel.AddWidgets(IndAttributePanels);

            CharCreatePanel.AddWidget(AttributePanel);

            //
            Panel[] CharModArray = new Panel[3];

            Panel SpeciesSelect = new Panel(10, 125, CharCreatePanel.Area.Width - 30,325);
            SpeciesSelect.AddWidget(new Label(1,100,"Select:"));


            Panel ClassSelect = new Panel(10, 125, CharCreatePanel.Area.Width - 30, 325);
            ClassSelect.AddWidget(new Button(-5, SpeciesSelect.Area.Height / 2, 10, "<", delegate { SpeciesSelect.Visible = true; ClassSelect.Visible = false; }));
            ClassSelect.Visible = false;

            Panel PerkSelect = new Panel(10, 125, CharCreatePanel.Area.Width - 30, 325);
            PerkSelect.AddWidget(new Button(-10, (SpeciesSelect.Area.Height / 2)-10, 10, "<", delegate { ClassSelect.Visible = true; PerkSelect.Visible = false; }));
            PerkSelect.Visible = false;

            CharModArray[0] = SpeciesSelect;
            CharModArray[1] = ClassSelect;
            CharModArray[2] = PerkSelect;

            CharCreatePanel.AddWidgets(CharModArray);

            SpeciesSelect.AddWidget(new Button(-5, SpeciesSelect.Area.Height / 2, 10, "<", delegate { }));

            gui.AddWidget(CharCreatePanel);
        }

        public override void OnResize()
        {
            gui.Resize();
            
        }

        private void SwitchMenus(Panel SwitchFrom, Panel SwitchTo) // as yet nonfunctional
        {
            CharCreatePanel.RemoveWidget(SwitchFrom);
            CharCreatePanel.AddWidget(SwitchTo);
        }

        public override void Update(GameTime time)
        {
            gui.Update(time);

            //Attribute label updates
            LabelAGEVAL.Value = Convert.ToString(Attributes.Age);
            LabelSTRVAL.Value = Convert.ToString(Attributes.Strength);
            LabelINTVAL.Value = Convert.ToString(Attributes.Intelligence);
            LabelCONVAL.Value = Convert.ToString(Attributes.Constitution);
            LabelDEXVAL.Value = Convert.ToString(Attributes.Dexterity);
            LabelCHAVAL.Value = Convert.ToString(Attributes.Charisma);
            OnResize();

        }
    }

    class MainMenuScreen : Screen
    {
        Gui gui;

        public override void OnResize()
        {
            gui.Resize();
        }

        public override void Update(GameTime time)
        {
            gui.Update(time);
        }

        public override void Draw()
        {
            gui.Draw();
        }

        public override void Init(Game1 game)
        {
            Color = Color.White;
            var skin = new Skin(game.GreyImageMap, game.GreyMap);
            var text = new Text(game.GreySpriteFont, Color.Chartreuse);

            gui = new Gui(game, skin, text);

            Panel mainmenupanel = new Panel(game.GraphicsDevice.Viewport.Bounds.Center.X-((game.GraphicsDevice.Viewport.Width / 3)/2),game.GraphicsDevice.Viewport.Bounds.Bottom-175,game.GraphicsDevice.Viewport.Width / 3,game.GraphicsDevice.Viewport.Height / 3);
            mainmenupanel.AddWidget(new Button(1, 1,mainmenupanel.Area.Width-10,"New Game", delegate 
            {
                GameManager.gameState = GameState.CharacterCreate;
                game.currentScreen = new CharCreateScreen();
                game.currentScreen.Init(game);
            }
            ));
            mainmenupanel.AddWidget(new Button(1, mainmenupanel.Area.Height / 4, mainmenupanel.Area.Width - 10, "Load Game"));
            mainmenupanel.AddWidget(new Button(1, (mainmenupanel.Area.Height / 4) * 2, mainmenupanel.Area.Width - 10, "Settings"));
            mainmenupanel.AddWidget(new Button(1, (mainmenupanel.Area.Height / 4) * 3, mainmenupanel.Area.Width - 10, "Quit"));

            gui.AddWidget(mainmenupanel); 
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FightingGame
{
    public class ScreenManager<TEnum> where TEnum : Enum
    {
        public Dictionary<TEnum, Screen<TEnum>> backingScreens = new Dictionary<TEnum, Screen<TEnum>>();

        public List<Screen<TEnum>> activeScreens = new List<Screen<TEnum>>();
        private int activeScreensIndex = -1;
        public Screen<TEnum> CurrentScreen => activeScreens[activeScreensIndex];
        public MouseState ms = new MouseState();

        public Screenum PreviousScreen;

        private ScreenManager()
        {

        }

        public static ScreenManager<TEnum> Instance { get; } = new ScreenManager<TEnum>();
        public void AddScreen(TEnum screenType, Screen<TEnum> screen)
        {
            if (backingScreens.ContainsKey(screenType))
            {
                return;
            }
            backingScreens.Add(screenType, screen);
        }

        public void ActivateScreen(Screen<TEnum> screen)
        {
            activeScreens.Add(screen);
            activeScreensIndex++;
        }

        public void ChangeScreen(TEnum newScreen, GraphicsDeviceManager graphics)
        {
            var pushingScreen = backingScreens[newScreen];
            pushingScreen.PreferedScreenSize(graphics);
            if (pushingScreen != activeScreens[activeScreensIndex])
            {
                pushingScreen.Initialize();
                activeScreens.Add(pushingScreen);
                activeScreensIndex++;
            }
        }

        public void GoBack()
        {
            if (activeScreensIndex > 0)
            {
                PreviousScreen = activeScreens[activeScreensIndex].ScreenType;
                activeScreens.RemoveAt(activeScreensIndex);
                activeScreensIndex--;
            }
        }

        public void Update(GraphicsDeviceManager graphics)
        {
            ms = Mouse.GetState();
            ChangeScreen(CurrentScreen.Update(ms), graphics);
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            int i;
            for (i = activeScreensIndex; i >= 0; i--)
            {
                if (!activeScreens[i].CanBeDrawnUnder)
                {
                    break;
                }
            }
            while (i <= activeScreensIndex)
            {
                activeScreens[i].Draw(spriteBatch);
                i++;
            }
        }
    }
}

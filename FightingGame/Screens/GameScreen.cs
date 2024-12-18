﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGame
{
    public class GameScreen : Screen<Screenum>
    {
        public GraphicsDeviceManager Graphics;
        public Camera Camera;

        private Matrix Transform;
        public override Screenum ScreenType { get; protected set; } = Screenum.GameScreen;
        public override bool IsActive { get; set; } = true;
        public override bool CanBeDrawnUnder { get; set; }

        Character SelectedCharacter;

        Dictionary<Keys, AnimationType> KeysToAnimation = new Dictionary<Keys, AnimationType>()
        {
            [Keys.W] = AnimationType.Run,
            [Keys.A] = AnimationType.Run,
            [Keys.D] = AnimationType.Run,
            [Keys.S] = AnimationType.Run,
            [Keys.I] = AnimationType.BasicAttack,
            [Keys.J] = AnimationType.Ability1,
            [Keys.K] = AnimationType.Ability2,
            [Keys.L] = AnimationType.Ability3,
            [Keys.R] = AnimationType.UltimateTransform,
            [Keys.F] = AnimationType.UndoTransform,
            [Keys.Space] = AnimationType.Dodge,
        };
        AnimationType currentAnimation = AnimationType.Stand;
        
        UIManager CharacterUIManager;
        ChestManager ChestManager;

        #region DrawableObjects
        DrawableObject Tilemap;
        #endregion

        public GameScreen(Dictionary<Texture, Texture2D> textures, GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            Tilemap = new DrawableObject(textures[Texture.GameScreenBackground], new Vector2(0, 0), new Vector2(1920 * 1.8f, 1920 * 1.8f), Color.White);
            Globals.Tilemap = Tilemap.HitBox;
        }
        public override void PreferedScreenSize(GraphicsDeviceManager graphics)
        {
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.IsFullScreen = true;
            //graphics.ApplyChanges();

        }
        public override void Initialize()
        {
            //Graphics.IsFullScreen = true;
            SelectedCharacter = ContentManager.Instance.Characters[EntityName.Hashashin];
            SelectedCharacter.SetBounds(new Rectangle(Tilemap.HitBox.X + 64, Tilemap.HitBox.Y + 64, Tilemap.HitBox.Width - 64, Tilemap.HitBox.Height - 64));
            SelectedCharacter.Reset();
            //SelectedCharacter.PowerUps.Add(PowerUpType.HealthRegenRateIncrease, new HealthRegenScript(PowerUpType.HealthRegenRateIncrease));

            Camera = new Camera(Graphics.GraphicsDevice.Viewport);
            Globals.Camera = Camera;
            CharacterUIManager = new UIManager(SelectedCharacter, Camera);
            GameObjects.Instance.EnemyManager = new EnemyManager(Tilemap);
            GameObjects.Instance.SelectedCharacter = SelectedCharacter;
            GameObjects.Instance.ProjectileManager = new ProjectileManager();
            GameObjects.Instance.DropManager = new DropManager();
            PowerUps.Instance.Reset();

            ChestManager = new ChestManager();
            for (int i = 0; i < ChestManager.maxChests; i++)
            {
                ChestManager.SpawnChest();
            }

        }
        public override Screenum Update(MouseState ms)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            InputManager.Update(Camera, SelectedCharacter);
            if (keysPressed.Length == 0)
            {
                currentAnimation = AnimationType.Stand;
            }
            else
            {    
                foreach (Keys key in keysPressed)
                {
                    if(KeysToAnimation.ContainsKey(key))
                    {
                        currentAnimation = KeysToAnimation[key];
                        if(InputManager.Moving && currentAnimation != AnimationType.Run)
                        {
                            break;
                        }
                    }
                    if(key == Keys.Q)
                    {
                        SelectedCharacter.TakeDamage(1, Color.White);
                    }
                }
            }
            if(SelectedCharacter.RemainingHealth <= 0)
            {
                currentAnimation = AnimationType.Death;
            }

            SelectedCharacter.Update(currentAnimation, InputManager.Direction);
            Camera.Update(SelectedCharacter.Position, Tilemap.HitBox);
            DamageNumberManager.Instance.Update();
            GameObjects.Instance.Update();
            ChestManager.Update();
            CharacterUIManager.Update(); 
            

            if(keysPressed.Contains(Keys.Escape))
            {
                return Screenum.EscapeScreen;
            }
            else if(SelectedCharacter.RemainingHealth <= 0)
            {
                return Screenum.GameOverScreen;
            }
            else if (SelectedCharacter.HasLeveledUp || GameObjects.Instance.DropManager.SelectedRarity != Rarity.None)
            {
                SelectedCharacter.HasLeveledUp = false;
                return Screenum.CardSelectionScreen;
            }
            else
            {
                return Screenum.GameScreen;
            } 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Transform = Camera.GetTransformMatrix();
            spriteBatch.Begin(transformMatrix: Transform , samplerState: SamplerState.PointClamp);

            Tilemap.Draw(spriteBatch);
            ChestManager.Draw();
            if(SelectedCharacter.RemainingHealth > 0)
            {
                SelectedCharacter.Draw();
            }
            DamageNumberManager.Instance.Draw();
            GameObjects.Instance.Draw();
            CharacterUIManager.Draw();


            spriteBatch.End();
        }
    }
}

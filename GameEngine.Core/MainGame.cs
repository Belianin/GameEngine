using System.Drawing;
using GameEngine.Core.Entities;
using GameEngine.Core.Fields;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace GameEngine.Core
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private PointF camera = new PointF();
        private GameField field;
        private Texture2D rectangle;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            field = GameFieldFactory.GetField(512, 256, 64);
            field.SpawnEntity(EntityFactory.SpawnPoint(100, 100, new Vector2(1, 0.5f)));
            field.SpawnEntity(EntityFactory.SpawnPoint(200, 150, new Vector2(-1, -0.5f)));
            
            rectangle = new Texture2D(graphics.GraphicsDevice, 1, 1);
            rectangle.SetData(new[] {Color.White});
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            field.Tick();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);

            spriteBatch.Begin();
            for (int x = 0; x < field.Chunks.GetLength(0); x++)
            {
                for (int y = 0; y < field.Chunks.GetLength(1); y++)
                {
                    var chunk = field.Chunks[x, y];
                    DrawOutlineRectangle(chunk.Start.X, chunk.Start.Y, chunk.Width, chunk.Height, 2, Color.Black);
                    foreach (var entity in chunk.Entities)
                    {
                        spriteBatch.Draw(rectangle, new Rectangle((int) entity.Position.X - 10, (int) entity.Position.Y - 10, 20, 20), Color.Red);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawOutlineRectangle(int x, int y, int width, int height, int size, Color color)
        {
            spriteBatch.Draw(rectangle, new Rectangle(x, y, width, size), color);
            spriteBatch.Draw(rectangle, new Rectangle(x, y, size, height), color);
            spriteBatch.Draw(rectangle, new Rectangle(x + width, y, size, height), color);
            spriteBatch.Draw(rectangle, new Rectangle(x, y + height, width, size), color);
        }
    }
}

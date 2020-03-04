using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade.Core;

namespace MonogameFacade
{
    public class GameFacade : Game
    {
        private readonly BaseGame BaseGame;
        public readonly Camera Camera;
        public Bag<Vector2> Touches = new Bag<Vector2>();
        private readonly QuadTree quadtree = new QuadTree(
            new Rectangle(-11000, -7000, 23000, 15000)
            , 50
            , 5
        );

        public GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        public GameFacade(BaseGame BaseGame)
        {
            this.BaseGame = BaseGame;
            this.Camera = new Camera();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            BaseGame.Font = Content.Load<SpriteFont>("font");
            BaseGame.Textures = BaseGame.LoadTextures(Content);

            BaseGame.Initialize();
            BaseGame.Ready(BaseGame);
        }

        private GameObject currentObject;
        private Collider currentCollider;

        public void ActualUpdate(GameTime gameTime)
        {
            quadtree.Clear();
            for (int i = 0; i < BaseGame.Objects.Count; i++)
                quadtree.AddRange(BaseGame.Objects[i].Colliders);

            for (int i = 0; i < BaseGame.Objects.Count; i++)
            {
                currentObject = BaseGame.Objects[i];
                currentObject.Update(BaseGame);

                currentObject.Location.Y =
                    currentObject.Location.Y
                    + currentObject.Velocity.Y;

                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        //currentCollider.BeforeCollisions();
                        CheckCollisions(CollisionDirection.Vertical, currentCollider);
                    }

                currentObject.Location.X =
                    currentObject.Location.X
                    + currentObject.Velocity.X;
                if (!currentObject.IsPassive)
                    for (int j = 0; j < currentObject.Colliders.Count; j++)
                    {
                        currentCollider = currentObject.Colliders[j];

                        //currentCollider.BeforeCollisions();
                        CheckCollisions(CollisionDirection.Horizontal, currentCollider);
                    }
            }

            Camera.Update(GraphicsDevice);
        }

        private void CheckCollisions(CollisionDirection direction, Collider source)
        {
            var targets = quadtree.Get(source);

            for (int i = 0; i < targets.Length; i++)
            {
                if (source.Parent == targets[i].Parent)
                    continue;

                if (direction == CollisionDirection.Vertical)
                    source.IsCollidingV(targets[i]);
                else
                    source.IsCollidingH(targets[i]);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            BaseGame.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            BaseGame.Draw(gameTime);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.transform);

            for (int i = 0; i < BaseGame.Objects.Count; i++)
                for (var j = 0; j < BaseGame.Objects[i].Renderers.Count; j++)
                    BaseGame.Objects[i].Renderers[j].Draw(spriteBatch, BaseGame.Objects[i]);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

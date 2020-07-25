using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;

namespace Skeletor
{
    public class SharedProperty<T>
    {
        public T Value;
    }

    //public class SpriteOnDrawMode : Renderer
    //{
    //    public Vector2 Location;
    //    public float Rotation;
    //    public Vector2 Scale;
    //    public Vector2 Origin;
    //    public Texture2D Texture = null;
    //    public List<SpriteOnDrawMode> Children = new List<SpriteOnDrawMode>();
    //    public SharedProperty<int> SelectedDepth = null;
    //    public int OwnDepth;

    //    public override void Begin()
    //    {
    //        base.Begin();
    //        this.Texture = null;
    //        this.SelectedDepth = null;
    //        Scale = new Vector2(1);
    //    }

    //    public override void End()
    //    {
            
    //    }

    //    // Transform = -Origin * Scale * Rotation * Translation
    //    public Matrix LocalTransform =>
    //        Matrix.CreateTranslation(-Texture.Width / 2f, -Texture.Height / 2f, 0f)
    //        * Matrix.CreateScale(Scale.X, Scale.Y, 1f)
    //        * Matrix.CreateRotationZ(Rotation)
    //        * Matrix.CreateTranslation(Location.X, Location.Y, 0f);

    //    public void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
    //    {
    //        // Calculate global transform
    //        Matrix globalTransform = LocalTransform * parentTransform;

    //        // Get values from GlobalTransform for SpriteBatch and render sprite
    //        Vector2 position;
    //        Vector2 scale;
    //        float rotation;
    //        DecomposeMatrix(ref globalTransform, out position, out rotation, out scale);


    //        spriteBatch.Draw(
    //            Texture
    //            , position
    //            , null
    //            , getColor()
    //            , rotation
    //            , Origin
    //            , scale
    //            , SpriteEffects.None
    //            , 0.0f
    //        );

    //        // Draw Children
    //        Children.ForEach(c => c.Draw(spriteBatch, globalTransform));
    //    }

    //    private Color getColor()
    //    {
    //        if (SelectedDepth.Value == OwnDepth)
    //            return Color.White;

    //        var diff = Math.Abs(SelectedDepth.Value - OwnDepth);

    //        if (diff == 1)
    //            return new Color(255, 255, 255, 128);

    //        return new Color(255, 255, 255, 64);
    //    }

    //    public static void DecomposeMatrix(
    //        ref Matrix matrix
    //        , out Vector2 position
    //        , out float rotation
    //        , out Vector2 scale
    //    )
    //    {
    //        Vector3 position3;
    //        Vector3 scale3;
    //        Quaternion rotationQ;
    //        matrix.Decompose(out scale3, out rotationQ, out position3);
    //        Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
    //        rotation = (float)Math.Atan2(direction.Y, direction.X);
    //        position = new Vector2(position3.X, position3.Y);
    //        scale = new Vector2(scale3.X, scale3.Y);
    //    }

    //    public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
    //    {
    //        Draw(batch, Matrix.Identity);
    //    }
    //}

    public class SpriteHierarchyRenderer : Renderer
    {
        public Vector2 Location;
        public float Rotation;
        public Vector2 Scale;
        public Vector2 Origin;
        public Texture2D Texture = null;
        public List<SpriteHierarchyRenderer> Children = new List<SpriteHierarchyRenderer>();

        private static readonly Pool<SpriteHierarchyRenderer> Pool = new Pool<SpriteHierarchyRenderer>();

        public static SpriteHierarchyRenderer GetFromPool() => Pool.Get();

        public SpriteHierarchyRenderer()
        {
            Reset();
        }

        public override void ReturnToPool()
        {
            Reset();
            Pool.Return(this);
        }

        private void Reset()
        {
            Scale = new Vector2(1);
            Texture = MonogameFacade.Game.DefaultTexture;
            Children.ForEach(f => f.ReturnToPool());
            Children.Clear();
        }

        // Transform = -Origin * Scale * Rotation * Translation
        [Obsolete("não poderei continuar pegando o tamanho da textura, quando virar um atras")]
        public Matrix LocalTransform =>
            Matrix.CreateTranslation(-Texture.Width / 2f, -Texture.Height / 2f, 0f)
            * Matrix.CreateScale(Scale.X, Scale.Y, 1f)
            * Matrix.CreateRotationZ(Rotation)
            * Matrix.CreateTranslation(Location.X, Location.Y, 0f);

        public void Draw(SpriteBatch spriteBatch, Matrix parentTransform)
        {
            // Calculate global transform
            Matrix globalTransform = LocalTransform * parentTransform;

            // Get values from GlobalTransform for SpriteBatch and render sprite
            Vector2 position;
            Vector2 scale;
            float rotation;
            DecomposeMatrix(ref globalTransform, out position, out rotation, out scale);

            spriteBatch.Draw(
                Texture
                , position
                , null
                , Color.White
                , rotation
                , Origin
                , scale
                , SpriteEffects.None
                , 0.0f
            );

            // Draw Children
            Children.ForEach(c => c.Draw(spriteBatch, globalTransform));
        }

        public static void DecomposeMatrix(
            ref Matrix matrix
            , out Vector2 position
            , out float rotation
            , out Vector2 scale
        )
        {
            Vector3 position3;
            Vector3 scale3;
            Quaternion rotationQ;
            matrix.Decompose(out scale3, out rotationQ, out position3);
            Vector2 direction = Vector2.Transform(Vector2.UnitX, rotationQ);
            rotation = (float)Math.Atan2(direction.Y, direction.X);
            position = new Vector2(position3.X, position3.Y);
            scale = new Vector2(scale3.X, scale3.Y);
        }

        public override void Draw(SpriteBatch batchGui, SpriteBatch batch, GameObject Parent)
        {
            Draw(batch, Matrix.Identity);
        }
    }

    //public class SpriteLineRenderer : Renderer
    //{
    //    public Texture2D texture = null;
    //    public Vector2 start;
    //    public Vector2 end;
    //    public Rectangle? source = null;

    //    public override void End()
    //    {
    //        texture = null;
    //        end = start = Vector2.Zero;
    //        source = null;
    //    }

    //    public override void Draw(
    //        SpriteBatch batchGui
    //        , SpriteBatch batch
    //        , GameObject Parent
    //    )
    //    {
    //        batch.Draw(
    //            texture
    //            , start
    //            , new Rectangle(
    //                (int)start.X
    //                , (int)start.Y
    //                , (int)Vector2.Distance(start, end)
    //                , 100
    //            )
    //            , Color.White
    //            , (float)Math.Atan2(
    //                end.Y - start.Y
    //                , end.X - start.X)
    //            , Vector2.Zero
    //            , 1.0f
    //            , SpriteEffects.None
    //            , 0
    //        );
    //    }
    //}
}

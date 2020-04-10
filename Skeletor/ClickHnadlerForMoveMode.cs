using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;

namespace Skeletor
{
    //TODO: "filemanager"
    //1- rectangle list
    //2- reorder with drag and drop
    //3- set as child with drag and drop
    //4- unset as child with drag and drop to "..."
    //5- think! how to rotate children with parent bone

    public class Bone : GameObject
    {
        //talvez nao precise disso....        
        public Bone ParentBone = null;
        public readonly List<Bone> Children = null;
        public readonly List<SpritePart> GluedSprites = null;
        public readonly SpriteLineRenderer BoneSprite;

        public Bone(Vector2 Start, Vector2 End, Texture2D texture)
        {         
            Children = new List<Bone>();
            GluedSprites = new List<SpritePart>();
            BoneSprite = new SpriteLineRenderer(texture);
            BoneSprite.start = Start;
            BoneSprite.end = End;
            Renderers.Add(BoneSprite);
        }
    }

    public class SkeletonAnimationParts
    {
        public Bone CurrentBone = null;

        public Bone AddNewBone(
            Vector2 start
            , Vector2 end
            , BaseGame game)
        {
            var newBone = new Bone(start, end, game.GetTexture("btn"));

            if (CurrentBone == null)
                CurrentBone = newBone;
            else
            {
                newBone.ParentBone = CurrentBone;
                CurrentBone.Children.Add(newBone);
            }

            game.Objects.Add(newBone);

            return newBone;
        }

        public void AddNewSprite(
            Point location
            , BaseGame game)
        {
            if (CurrentBone == null)
                return;
            var newSprite = new SpritePart(game);
            newSprite.Location = location;
            CurrentBone.GluedSprites.Add(newSprite);
            game.Objects.Add(newSprite);
        }

        public void GoToChildren(int index)
        {
        }

        public void Rotate()
        {
            //CurrentBone.Rotate();
        }

        public void GoToParent()
        {
        }
    }

    public class ClickHnadlerForMoveMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly SpriteRenderer sprite;

        public ClickHnadlerForMoveMode(DisplayMode mode, BaseGame game)
        {
            this.mode = mode;
            sprite = new SpriteRenderer();
            sprite.Texture = game.GetTexture("btn");
            sprite.Size = new Microsoft.Xna.Framework.Point(1000);
            sprite.Offset = new Microsoft.Xna.Framework.Point(200);
        }

        public override void Update(BaseGame game)
        {
            Renderers.Clear();

            if (mode.mode != DisplayModeEnum.Move)
            {
                return;
            }

            Renderers.Add(sprite);

        }
    }
}

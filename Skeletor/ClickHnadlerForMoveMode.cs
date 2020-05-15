using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFacade;
using System;
using System.Collections.Generic;

namespace Skeletor
{
    //TODO: change from bone animation to thumbtack animation?
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

        public void Rotate()
        {
            //BoneSprite.
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
            if (CurrentBone != null)
                CurrentBone.Rotate();
        }

        public void GoToParent()
        {
        }
    }

    public class ClickHnadlerForMoveMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly SkeletonAnimationParts skeleton;

        public ClickHnadlerForMoveMode(
            DisplayMode mode
            , BaseGame game
            , SkeletonAnimationParts skeleton)
        {
            this.mode = mode;
            this.skeleton = skeleton;
        }

        public override void Update(BaseGame game)
        {
            if (mode.mode != DisplayModeEnum.Move)
                return;

            skeleton.Rotate();
        }
    }
}

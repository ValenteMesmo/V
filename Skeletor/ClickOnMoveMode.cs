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


    public class Bone
    {
        public readonly List<Bone> Bones = null;
        public readonly List<string> Sprites = null;

        public Bone()
        {
            Bones = new List<Bone>();
            Sprites = new List<string>();
        }
    }

    public class BonesHierarchy
    {
        public Bone Bone = null;

        public BonesHierarchy()
        {
        }

        public void AddNewBone()
        {
            Bone = new Bone();
        }

        public void AddNewSprite()
        {

        }

        public void NavigateToBone(int index)
        {
        }
    }

    public class ClickOnMoveMode : GameObject
    {
        private readonly DisplayMode mode = null;
        private readonly SpriteRenderer sprite;

        public ClickOnMoveMode(DisplayMode mode, BaseGame game)
        {
            this.mode = mode;
            sprite = new SpriteRenderer();
            sprite.Texture = game.Textures["btn"];
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

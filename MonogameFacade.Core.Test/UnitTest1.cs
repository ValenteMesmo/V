//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using System.Collections.Generic;
//using Xunit;

//namespace MonogameFacade.Core.Test
//{
//    public class DpadTests
//    {
//        [Fact]
//        public void LeftFromNone()
//        {
//            var game = new TestGame();
//            var sut = new Dpad(game, new Systems.InputKeeper());

//            var touch = (sut.touchArea.Center + new Point(Dpad.minDistance + 1, 0))
//                .ToVector2();

//            game.TouchesUi.Add(touch);

//            Assert.Equal(DpadDirection.None, sut.CurrentDirection);
//            sut.Update(game);

//            Assert.Equal(DpadDirection.Right, sut.CurrentDirection);
//        }

//        [Fact]
//        public void RightFromRight()
//        {
//            var game = new TestGame();
//            var sut = new Dpad(game, new Systems.InputKeeper());

//            var touch = (sut.touchArea.Center + new Point(Dpad.minDistance + 1, 0))
//                .ToVector2();

//            game.TouchesUi.Add(touch);

//            Assert.Equal(DpadDirection.None, sut.CurrentDirection);
//            sut.Update(game);
//            sut.Update(game);
//            sut.Update(game);

//            Assert.Equal(DpadDirection.Right, sut.CurrentDirection);
//        }

//        [Fact]
//        public void NoneFromRight()
//        {
//            var game = new TestGame();
//            var sut = new Dpad(game, new Systems.InputKeeper());

//            var touch = (sut.touchArea.Center + new Point(Dpad.minDistance + 1, 0))
//                .ToVector2();

//            game.TouchesUi.Add(touch);

//            Assert.Equal(DpadDirection.None, sut.CurrentDirection);
//            sut.Update(game);
//            game.TouchesUi.Clear();
//            sut.Update(game);

//            Assert.Equal(DpadDirection.None, sut.CurrentDirection);
//        }
//    }
//}

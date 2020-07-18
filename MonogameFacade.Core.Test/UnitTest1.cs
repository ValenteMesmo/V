using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.Xna.Framework;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace MonogameFacade.Core.Test
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(new AutoNSubstituteCustomization());
                var touchs = new List<Point>();
                var game = fixture.Freeze<IGame>();
                fixture.Register(() => new GamePadData { input = new InputKeeper() });
                game.TouchesUi.Returns(touchs);
                Game.Instance = game;
                return fixture;
            })
        { }
    }


    public class DpadTests
    {
        [Theory, AutoNSubstituteData]
        public void TouchRightFromNoTouch(IGame game, GamePadData data)
        {
            DirectionalTouchButtons.Create(data);

            game.TouchesUi.Add(
                new Point(
                    data.touchArea.Center.X + data.touchArea.Width / 3
                    , data.touchArea.Center.Y
                )
            );

            Assert.Equal(GamePadDirection.None, data.CurrentDirection);

            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.Right, data.CurrentDirection);
        }

        [Theory, AutoNSubstituteData]
        public void TouchRightContinuous(IGame game, GamePadData data)
        {
            DirectionalTouchButtons.Create(data);

            game.TouchesUi.Add(
                new Point(
                    data.touchArea.Center.X + data.touchArea.Width / 3
                    , data.touchArea.Center.Y
                )
            );

            Assert.Equal(GamePadDirection.None, data.CurrentDirection);

            BaseTouchButtons.Update(data);
            BaseTouchButtons.Update(data);
            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.Right, data.CurrentDirection);
        }

        [Theory, AutoNSubstituteData]
        public void ReleaseTouchFromRightTouch(IGame game, GamePadData data)
        {
            DirectionalTouchButtons.Create(data);

            game.TouchesUi.Add(
                new Point(
                    data.touchArea.Center.X + data.touchArea.Width / 3
                    , data.touchArea.Center.Y
                )
            );

            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.Right, data.CurrentDirection);

            game.TouchesUi.Clear();

            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.None, data.CurrentDirection);
        }

        [Theory, AutoNSubstituteData]
        public void TouchDiagonalAfterRightRelease(IGame game, GamePadData data)
        {
            DirectionalTouchButtons.Create(data);

            var rightTouch = new Point(
               data.touchArea.Center.X + (data.touchArea.Size.X / 4)
               , data.touchArea.Center.Y
           );

            var diagonalTouch = new Point(
                data.touchArea.Center.X + (data.touchArea.Size.X / 4)
                , data.touchArea.Center.Y + (data.touchArea.Size.Y / 3)
            );

            game.TouchesUi.Add(
                rightTouch
            );

            BaseTouchButtons.Update(data);
            game.TouchesUi.Clear();
            BaseTouchButtons.Update(data);

            game.TouchesUi.Add(
               diagonalTouch
            );
            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.Down, data.CurrentDirection);
        }
    }
}

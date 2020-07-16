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
                var touchs = new List<Vector2>();
                var game = fixture.Freeze<IGame>();
                fixture.Register(() => new GamePadData { input = new InputKeeper()});
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
                (data.touchArea.Center + new Point(BaseTouchButtons.minDistance + 1, 0))
                .ToVector2()
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
                (data.touchArea.Center + new Point(BaseTouchButtons.minDistance + 1, 0))
                .ToVector2()
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
                (data.touchArea.Center + new Point(BaseTouchButtons.minDistance + 1, 0))
                .ToVector2()
            );

            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.Right, data.CurrentDirection);

            game.TouchesUi.Clear();

            BaseTouchButtons.Update(data);

            Assert.Equal(GamePadDirection.None, data.CurrentDirection);
        }
    }
}

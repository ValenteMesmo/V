using AutoFixture.Xunit2;
using Microsoft.Xna.Framework;
using MonogameFacade;
using System;
using Xunit;

namespace Skeletor.Test
{
    using SutAttribute = NoAutoPropertiesAttribute;

    public class BonesHierarchyTest
    {
        [Theory, AutoNSubstituteData]
        public void AddNewBone(
            [Sut]SkeletonAnimationParts sut
            , Vector2 from
            , Vector2 to
            , BaseGame game)
        {
            Assert.Null(sut.CurrentBone);
            sut.AddNewBone(from, to, game);
            Assert.NotNull(sut.CurrentBone);
        }

        [Theory, AutoNSubstituteData]
        public void AddNewSpriteWithoutAnyBones(
            [Sut]SkeletonAnimationParts sut
            , Vector2 location)
        {
            sut.AddNewSprite(location);
            Assert.Null(sut.CurrentBone);
        }

        [Theory, AutoNSubstituteData]
        public void AddNewSprite(
            [Sut]SkeletonAnimationParts sut
            , Vector2 from
            , Vector2 to
            , Vector2 spriteLocation
            , BaseGame game)
        {
            sut.AddNewBone(from, to, game);
            sut.AddNewSprite(spriteLocation);
        }

        [Theory, AutoNSubstituteData]
        public void NavigateToBone(
            [Sut]SkeletonAnimationParts sut
            , Vector2 from
            , Vector2 to
            , BaseGame game)
        {
            sut.AddNewBone(from, to, game);
            sut.GoToChildren(0);
        }
    }
}

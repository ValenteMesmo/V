using AutoFixture.Xunit2;
using System;
using Xunit;

namespace Skeletor.Test
{
    public class BonesHierarchyTest
    {
        [Theory, AutoNSubstituteData]
        public void NavigateToBone([NoAutoProperties]BonesHierarchy sut)
        {
            sut.AddNewBone();
            sut.NavigateToBone(0);
        }


        [Theory, AutoNSubstituteData]
        public void AddNewBone([NoAutoProperties]BonesHierarchy sut)
        {
            Assert.Null(sut.Bone);
            sut.AddNewBone();
            Assert.NotNull(sut.Bone);
        }

        [Theory, AutoNSubstituteData]
        public void AddNewSpriteWithoutAnyBones([NoAutoProperties]BonesHierarchy sut)
        {
            sut.AddNewSprite();
            Assert.Null(sut.Bone);
        }

        [Theory, AutoNSubstituteData]
        public void AddNewSprite([NoAutoProperties]BonesHierarchy sut)
        {
            sut.AddNewSprite();
        }
    }
}

﻿using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Skeletor.Test
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(
                  () => new Fixture()
                  .Customize(
                      new AutoNSubstituteCustomization()))
        {
        }
    }
}

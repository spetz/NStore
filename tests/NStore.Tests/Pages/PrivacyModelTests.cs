using System;
using FluentAssertions;
using NStore.Web.Pages;
using Xunit;

namespace NStore.Tests.Pages
{
    public class PrivacyModelTests
    {
        [Fact]
        public void on_post_should_always_fail()
        {
            var model = new PrivacyModel();

            Action onPost = () => model.OnPost();

            onPost.Should().Throw<Exception>()
                .And.Message.Should().Be("Oooppsss...");
        }
    }
}
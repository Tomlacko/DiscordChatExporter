﻿using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using DiscordChatExporter.Cli.Tests.Fixtures;
using DiscordChatExporter.Cli.Tests.TestData;
using FluentAssertions;
using Xunit;

namespace DiscordChatExporter.Cli.Tests.Specs.HtmlWriting
{
    public record GeneralSpecs(ExportWrapperFixture ExportWrapper) : IClassFixture<ExportWrapperFixture>
    {
        [Fact]
        public async Task Messages_are_exported_correctly()
        {
            // Act
            var document = await ExportWrapper.ExportAsHtmlAsync(ChannelIds.DateRangeTestCases);

            var messageIds = document
                .QuerySelectorAll(".chatlog__message")
                .Select(e => e.GetAttribute("data-message-id"))
                .ToArray();

            var messageTexts = document
                .QuerySelectorAll(".chatlog__content")
                .Select(e => e.Text().Trim())
                .ToArray();

            // Assert
            messageIds.Should().Equal(
                "866674314627121232",
                "866710679758045195",
                "866732113319428096",
                "868490009366396958",
                "868505966528835604",
                "868505969821364245",
                "868505973294268457",
                "885169254029213696"
            );

            messageTexts.Should().Equal(
                "Hello world",
                "Goodbye world",
                "Foo bar",
                "Hurdle Durdle",
                "One",
                "Two",
                "Three",
                "Yeet"
            );
        }
    }
}
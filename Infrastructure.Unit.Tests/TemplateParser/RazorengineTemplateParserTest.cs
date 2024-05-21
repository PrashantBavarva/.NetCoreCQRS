using Common.Logging;
using FluentAssertions;
using Infrastructure.TemplateParser;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Unit.Tests.TemplateParser
{
    public class RazorengineTemplateParserTest
    {
        private readonly ILoggerAdapter<RazorEngineTemplateParser> _logger =
            Substitute.For<ILoggerAdapter<RazorEngineTemplateParser>>();
        private readonly RazorEngineTemplateParser _sut;

        public RazorengineTemplateParserTest()
        {
            _sut = new RazorEngineTemplateParser(_logger);
        }

        [Fact]
        public async Task ParseAsync_ShouldSuccess_WhenParsedTemplate()
        {
            // Arrange
            var key = "key";
            var content = "Hello @Model.Name";
            var model = new { Name = "World" };

            // Act
            var result = await _sut.ParseAsync(key, content, model);

            // Assert
            result.Should().Be("Hello World");
        }

        [Fact]
        public async Task ParseAsync_ShouldUseCache_WhenParsedTemplate()
        {
            // Arrange
            var key = "key";
            var content = "Hello @Model.Name";
            var model = new { Name = "World" };
            var model2 = new { Name = "Mohamed" };

            // Act
            var result = await _sut.ParseAsync(key, content, model);
            var result2 = await _sut.ParseAsync(key, content, model2);

            // Assert
            result.Should().Be("Hello World");
            result2.Should().Be("Hello Mohamed");
            _logger.Received(1).LogDebug("Compiling template {TemplateKey}", key);
        }

        [Fact]
        public async Task LoadAndParseAsync_ShouldSuccess_WhenParsedTemplate()
        {
            // Arrange
            var key = "key";
            var filePath = "test.txt";
            var model = new { Name = "World" };
            var content = "Hello @Model.Name";
            await File.WriteAllTextAsync(filePath, content);

            // Act
            var result = await _sut.LoadAndParseAsync(key, filePath, model);

            // Assert
            result.Should().Be("Hello World");
        }

        [Fact]
        public async Task LoadAndParseAsync_ShouldUseCache_WhenParsedTemplate()
        {
            // Arrange
            var key = "key";
            var filePath = "test.txt";
            var model = new { Name = "World" };
            var model2 = new { Name = "Mohamed" };
            var content = "Hello @Model.Name";
            await File.WriteAllTextAsync(filePath, content);

            // Act
            var result = await _sut.LoadAndParseAsync(key, filePath, model);
            var result2 = await _sut.LoadAndParseAsync(key, filePath, model2);

            // Assert
            result.Should().Be("Hello World");
            result2.Should().Be("Hello Mohamed");
            _logger.Received(1).LogDebug("Compiling template {TemplateKey}", key);

        }
        [Fact]
        public async Task LoadAndParseAsync_ShouldUseCache_WhenParsedTemplateForArabicVersion()
        {
            // Arrange
            var key = "key";
            var filePath = "test.txt";
            var model = new { Name = "عالم" };
            var model2 = new { Name = "عربي" };
            var content = "مرحبا @Model.Name";
            await File.WriteAllTextAsync(filePath, content);

            // Act
            var result = await _sut.LoadAndParseAsync(key, filePath, model);
            var result2 = await _sut.LoadAndParseAsync(key, filePath, model2);

            // Assert
            result.Should().Be("مرحبا عالم");
            result2.Should().Be("مرحبا عربي");
            _logger.Received(1).LogDebug("Compiling template {TemplateKey}", key);

        }
    }
}

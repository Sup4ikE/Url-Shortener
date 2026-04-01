using FluentAssertions;
using Moq;
using UrlShortener.Core.Application.Interfaces;
using UrlShortener.Core.Application.Services;
using UrlShortener.Core.Domain.Entities;

public class ShortUrlServiceTests
{
    private readonly Mock<IShortUrlRepository> _repoMock = new();
    private readonly Mock<ICodeGenerator> _generatorMock = new();

    private ShortUrlService CreateService()
    {
        return new ShortUrlService(_repoMock.Object, _generatorMock.Object);
    }

    [Fact]
    public async Task CreateAsync_Should_Create_ShortUrl()
    {
        // Arrange
        var service = CreateService();

        _generatorMock
            .Setup(x => x.Generate(It.IsAny<int>()))
            .Returns("abc123");
        _repoMock.Setup(x => x.ExistsByCodeAsync(It.IsAny<string>())).ReturnsAsync(false);
        _repoMock.Setup(x => x.ExistsByOriginalUrlAsync(It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await service.CreateAsync("https://google.com", 1);

        // Assert
        result.ShortCode.Should().Be("abc123");
        _repoMock.Verify(x => x.AddAsync(It.IsAny<ShortUrl>()), Times.Once);
        _repoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
    
    [Fact]
    public async Task CreateAsync_Should_Throw_When_Url_Already_Exists()
    {
        // Arrange
        var service = CreateService();

        _repoMock.Setup(x => x.ExistsByOriginalUrlAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () =>
            await service.CreateAsync("https://google.com", 1);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("URL already exists");
    }
    
    [Fact]
    public async Task DeleteAsync_Should_Delete_When_User_Is_Owner()
    {
        // Arrange
        var service = CreateService();

        var entity = new ShortUrl
        {
            ShortCode = "abc",
            CreatedById = 1
        };

        _repoMock.Setup(x => x.GetByCodeAsync("abc"))
            .ReturnsAsync(entity);

        // Act
        var result = await service.DeleteAsync("abc", 1, "User");

        // Assert
        result.Should().BeTrue();
        _repoMock.Verify(x => x.DeleteAsync(entity), Times.Once);
    }
}
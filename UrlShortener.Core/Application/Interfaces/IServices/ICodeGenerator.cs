namespace UrlShortener.Core.Application.Interfaces;

public interface ICodeGenerator
{
    string Generate(int length = 6);
}
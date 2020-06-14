using NewsSolution.Model;

namespace NewsSolution.Core
{
    public interface INewsProvider
    {
        ResponseDto GetData();
    }
}
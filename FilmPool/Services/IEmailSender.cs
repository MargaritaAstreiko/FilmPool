using FilmPool.JWT;
using FilmPool.RequestModels;

namespace FilmPool.Services
{
  public interface IEmailSender
  {
    void SendEmail(Message message);
  }
}

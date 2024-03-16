namespace SimpleShop.Application.Common.Interfaces;

// do wysyłania emaili z aplikacji
public interface IEmail
{
	Task Send(string subject, string body, string to, string attachmentPath = null);
}
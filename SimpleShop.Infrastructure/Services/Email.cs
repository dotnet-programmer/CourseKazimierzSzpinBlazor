﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SimpleShop.Application.Common.Interfaces;

namespace SimpleShop.Infrastructure.Services;

public class Email : IEmail
{
	private readonly string _hostSmtp;
	private readonly int _port;
	private readonly string _senderEmail;
	private readonly string _senderEmailPassword;
	private readonly string _senderName;

	public Email(IConfiguration configuration)
	{
		// dane o wysyłce mailowej zapisane w pliku konfiguracyjnym
		_hostSmtp = configuration["Email:HostSmtp"];
		_port = int.Parse(configuration["Email:Port"]);
		_senderEmail = configuration["Email:SenderEmail"];
		_senderEmailPassword = configuration["Email:SenderEmailPassword"];
		_senderName = configuration["Email:SenderName"];
	}

	public async Task Send(string subject, string body, string to, string attachmentPath = null)
	{
		MimeMessage message = new();
		message.From.Add(new MailboxAddress(_senderName, _senderEmail));
		message.To.Add(MailboxAddress.Parse(to));
		message.Subject = subject;

		BodyBuilder builder = new()
		{
			HtmlBody = @$"
                <html>
                     <head>
                     </head>
                     <body>
                        <div style=""font-size: 16px; padding: 10px; font-family: Arial; line-height: 1.4;"">
                            {body}
                        </div>
                     </body>
                </html>
                "
		};

		if (!string.IsNullOrWhiteSpace(attachmentPath))
		{
			builder.Attachments.Add(attachmentPath);
		}

		message.Body = builder.ToMessageBody();

		using (SmtpClient client = new())
		{
			await client.ConnectAsync(_hostSmtp, _port, SecureSocketOptions.Auto);
			await client.AuthenticateAsync(_senderEmail, _senderEmailPassword);
			await client.SendAsync(message);
			await client.DisconnectAsync(true);
		}
	}
}
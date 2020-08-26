using MailKit.Net.Imap;
using System;

namespace DCXWRUATF.UITests.Support
{
    public class EmailClient : IDisposable
    {
        private bool isDisposed;

        protected internal ImapClient Imap { get; } = new ImapClient
        {
            ServerCertificateValidationCallback = (s, c, h, e) => true
        };

        public string Email { get; private set; }

        public EmailClient Connect(string host, int port, bool useSsl)
        {
            Imap.Connect(host, port, useSsl);
            return this;
        }

        public EmailContext Authenticate(string email, string password)
        {
            Email = email;

            Imap.Authenticate(email, password);

            return new EmailContext(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Imap.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}

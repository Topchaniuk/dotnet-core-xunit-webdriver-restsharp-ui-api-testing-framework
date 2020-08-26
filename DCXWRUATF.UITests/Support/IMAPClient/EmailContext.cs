using MailKit;

namespace DCXWRUATF.UITests.Support
{
    public class EmailContext
    {
        public EmailContext(EmailClient client)
        {
            Client = client;
        }

        public EmailContext(EmailContext context)
        {
            Client = context.Client;
        }

        public EmailClient Client { get; }

        public EmailFolderContext OpenInbox()
        {
            IMailFolder folder = Client.Imap.Inbox;
            folder.Open(FolderAccess.ReadOnly);

            return new EmailFolderContext(this, folder);
        }
    }
}

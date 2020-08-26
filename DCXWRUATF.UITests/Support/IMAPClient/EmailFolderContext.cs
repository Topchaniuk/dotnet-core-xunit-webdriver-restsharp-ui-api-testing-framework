using MailKit;
using MailKit.Search;
using System;

namespace DCXWRUATF.UITests.Support
{
    public class EmailFolderContext : EmailContext
    {
        public EmailFolderContext(EmailFolderContext context)
            : this(context, context.Folder)
        {
        }

        public EmailFolderContext(EmailContext context, IMailFolder folder)
            : base(context)
        {
            Folder = folder ?? throw new InvalidOperationException($"Unknown folder to search in. Active folder is not set. Invoke {nameof(OpenInbox)} method for example.");
        }

        protected IMailFolder Folder { get; }

        public bool FindUnread()
        {
            int unreadCount = Folder.Search(SearchQuery.NotSeen).Count;
            return unreadCount > 0;
        }

        private IMailFolder CheckFolder()
        {
            Folder.Check();
            return Folder;
        }
    }
}

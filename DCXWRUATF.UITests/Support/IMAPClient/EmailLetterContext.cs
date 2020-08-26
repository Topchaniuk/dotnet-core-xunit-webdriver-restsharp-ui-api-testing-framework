using MimeKit;
using DCXWRUATF.UITests.Pages;
using System.Linq;

namespace DCXWRUATF.UITests.Support
{
    public class EmailLetterContext : EmailFolderContext
    {
        public EmailLetterContext(EmailFolderContext context, MimeMessage message)
            : base(context)
        {
            Message = message;
        }

        protected MimeMessage Message { get; }

        public EmailLetterContext VerifyAttachment(string attachmentName)
        {
            var attachments = Message.Attachments;

            if (attachments == null || !attachments.Any())
                BasePage.CallStack("Letter does not contain any attachments.");
            else if (!attachments.Any(x => x.ContentType.Name == attachmentName))
                BasePage.CallStack($"Letter does not contain '{attachmentName}' attachment.");

            return this;
        }
    }
}

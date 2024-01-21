namespace Email.Core.Dtos;
public class EmailMessage
{
    public string From { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsHtml { get; set; } = false;
    public List<string> Cc { get; set; }
    public List<string> Bcc { get; set; }
    public List<EmailAttachment> Attachments { get; set; }
}

public class EmailAttachment
{
    public string FileName { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }
}

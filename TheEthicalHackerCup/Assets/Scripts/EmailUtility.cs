using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class EmailUtility : MonoBehaviour
{
    private Button btn;
    private MailAddress from;
    private MailAddress to;
    private MailMessage message;
    private SmtpClient client;
    // Start is called before the first frame update
    void Start()
    {
        // Command-line argument must be the SMTP host.
        client = new SmtpClient("smtp.office365.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "ethicalhackingcup@outlook.com", 
            "Ethicalpassword");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        from = new MailAddress(
            "ethicalhackingcup@outlook.com",
            "Ethical Hacker",
            System.Text.Encoding.UTF8);
        
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
    }

    public void SendEmail(string subject, string body) {
        // Set destinations for the email message.
        string email = GameManager.GetInstance().GetPlayerEmail();
        to = new MailAddress(email);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "test message1";
        // Specify the message content.
        message = new MailMessage(from, to);
        message.Body = body;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = subject;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        client.SendAsync(message, userState);
    }

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled "+ token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ "+token+" ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;

public static class MailMessageExtensions
{
	public static string ToEmlString(this MailMessage message)
	{
		// create a temp folder to hold just this .eml file so that we can find it easily.
		string temp_folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		// Make sure our folder is nice and clean... and empty
		if (Directory.Exists(temp_folder))
		{
			Directory.Delete(temp_folder);
		}
		if (Directory.Exists(temp_folder))
		{
			throw new IOException("Unable to delete temporary folder");
		}
		Directory.CreateDirectory(temp_folder);

		// Write message to file using SmtpClient
		using (SmtpClient smtp_client = new SmtpClient("workaround for .net 4.0 bug"))
		{
			smtp_client.UseDefaultCredentials = true;
			smtp_client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
			smtp_client.PickupDirectoryLocation = temp_folder;
			//smtp_client.Enco
			smtp_client.Send(message);
		}

		// Get file from temp folder. Must be the only file
		// We previously deleted the folder to be sure about that
		string temp_file = Directory.GetFiles(temp_folder).Single();

		// Make sure the file actually exists
		if (!File.Exists(temp_file))
		{
			throw new IOException("Temporary file not found");
		}

		string result = File.ReadAllText(temp_file);

		// Clean up our mess
		Directory.Delete(temp_folder, true);

		return result;
	}

	public static string ToEmlStringRefactored(this MailMessage message)
	{
		// create a temp folder to hold just this .eml file so that we can find it easily.
		string temp_folder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

		// Make sure our folder is nice and clean... and empty
		if (Directory.Exists(temp_folder))
		{
			Directory.Delete(temp_folder);
		}
		if (Directory.Exists(temp_folder))
		{
			throw new IOException("Unable to delete temporary folder");
		}
		Directory.CreateDirectory(temp_folder);

		// Create unique file
		string temp_file = Path.Combine(temp_folder, Path.GetFileName(Guid.NewGuid().ToString()));

		message.Save(temp_file);

		// Make sure the file actually exists
		if (!File.Exists(temp_file))
		{
			throw new IOException("Unable to create eml file");
		}

		string result = File.ReadAllText(temp_file);

		// Clean up our mess
		Directory.Delete(temp_folder, true);

		return result;
	}

	public static void Save(this MailMessage Message, string FileName)
	{
		Assembly assembly = typeof(SmtpClient).Assembly;
		Type mail_writer_type =
			assembly.GetType("System.Net.Mail.MailWriter");

		using (FileStream file_stream =
					 new FileStream(FileName, FileMode.Create))
		{
			// Get reflection info for MailWriter contructor
			ConstructorInfo mail_writer_constructor =
					mail_writer_type.GetConstructor(
							BindingFlags.Instance | BindingFlags.NonPublic,
							null,
							new Type[] { typeof(Stream) },
							null);

			// Construct MailWriter object with our FileStream
			object mail_writer =
				mail_writer_constructor.Invoke(new object[] { file_stream });

			// Get reflection info for Send() method on MailMessage
			MethodInfo send_method =
					typeof(MailMessage).GetMethod(
							"Send",
							BindingFlags.Instance | BindingFlags.NonPublic);

			// Call method passing in MailWriter
			send_method.Invoke(
					Message,
					BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					new object[] { mail_writer, true, true },
					null);

			// Finally get reflection info for Close() method on our MailWriter
			MethodInfo close_method =
					mail_writer.GetType().GetMethod(
							"Close",
							BindingFlags.Instance | BindingFlags.NonPublic);

			// Call close method
			close_method.Invoke(
					mail_writer,
					BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					new object[] { },
					null);
		}
	}
}
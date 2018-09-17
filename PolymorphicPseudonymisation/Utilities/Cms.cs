﻿using System.IO;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Security;

namespace PolymorphicPseudonymisation.Utilities
{
    public static class Cms
    {
        public static string ConvertToPem(string p7File, string p8File)
        {
            var p7Data = File.ReadAllBytes(p7File);
            var p8Data = File.ReadAllBytes(p8File);

            var privateKey = PrivateKeyFactory.CreateKey(p8Data);

            var parser = new CmsEnvelopedDataParser(p7Data);
            var recipients = parser.GetRecipientInfos().GetRecipients().OfType<RecipientInformation>();
            var recipientInformation = recipients.First();
            var keyInfo = (KeyTransRecipientInformation)recipientInformation;

            //TODO: Not sure what this did in Java?
            //final byte[] message = keyInfo.getContent(new JceKeyTransEnvelopedRecipient(key).setProvider("BC"));
            var message = keyInfo.GetContent(privateKey);

            return Encoding.ASCII.GetString(message);
        }
    }
}
﻿using Org.BouncyCastle.Math.EC;
using PolymorphicPseudonymisation.Exceptions;
using PolymorphicPseudonymisation.Key;
using PolymorphicPseudonymisation.Parser;
using System;

namespace PolymorphicPseudonymisation.Entity
{
    public class EncryptedEntity : Identifiable
    {
        public ECPoint[] Points { get; set; }

        public static T FromBase64<T>(string base64, EncryptedVerifier verifier) where T : EncryptedEntity
        {
            var key = FromBase64(base64, verifier);
            if (!(key is T))
            {
                throw new PolymorphicPseudonymisationException(
                    $"Expected instance of {typeof(T).Name}, got {key.GetType().Name}");
            }

            return (T)key;
        }

        private static EncryptedEntity FromBase64(string base64, EncryptedVerifier verifier)
        {
            var encoded = Convert.FromBase64String(base64);
            return EncryptedEntityParser.Decode(encoded, verifier);
        }

        public EncryptedEntity()
        {
            Points = new ECPoint[3];
        }
    }
}
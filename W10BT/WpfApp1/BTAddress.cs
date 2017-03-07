using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class BtAddress
    {
        /// <summary>
        ///     This Bluetooth device address represented as a six-byte array.
        /// </summary>
        public byte[] AddressBytes { get; }

        /// <summary>
        ///     Creates a new <see cref="BtAddress" /> from its ordered bytes.
        /// </summary>
        /// <param name="addressBytes">The ordered bytes of the address, or a byte array containing them.</param>
        public BtAddress(params byte[] addressBytes)
        {
            this.AddressBytes = new byte[6];
            Array.Copy(addressBytes, this.AddressBytes, 6);
        }

        /// <summary>
        ///     Create a new <see cref="BtAddress" /> from the first six bytes of a <see cref="ulong" />,
        ///     discarding the final two.
        /// </summary>
        /// <param name="value">The <see cref="ulong" /> in which the <see cref="BtAddress" /> is encoded.</param>
        public BtAddress(ulong value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            this.AddressBytes = new byte[6];
            Array.Copy(bytes, this.AddressBytes, 6);
        }

        /// <summary>
        ///     Return a <see cref="BtAddress" /> parsed from the string representation of a Bluetooth device
        ///     address in MAC-48 format.
        /// </summary>
        /// <param name="address">The Bluetooth device address string to parse.</param>
        /// <returns>The <see cref="BtAddress" /> corresponding to <paramref name="address" />.</returns>
        public static BtAddress Parse(string address)
        {
            string[] digits = address.Split(':', '-');

            var bytes = new byte[6];
            for (var i = 0; i < 6; i++)

                // ReSharper disable once PossibleNullReferenceException - Mac48Address aspect guarantees that there will be
                // exactly six groups, all composed of hex digits.
                bytes[i] = byte.Parse(digits[i].Trim(), NumberStyles.HexNumber);

            return new BtAddress(bytes);
        }

        /// <summary>
        ///     Attempt to parse the string representation of a Bluetooth device address in MAC-48 format.
        /// </summary>
        /// <param name="address">The Bluetooth device address string to parse.</param>
        /// <returns>
        ///     A tuple consisting of a boolean success indicator and the parsed <see cref="BtAddress" /> (if successful),
        ///     or the null (all zeroes) MAC-48 address on failure.
        /// </returns>
        public static BtAddress TryParse(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return new BtAddress(0, 0, 0, 0, 0, 0);

            try
            {
                BtAddress result = BtAddress.Parse(address);
                return result;
            }
            catch (ArgumentException)
            {
                return new BtAddress(0, 0, 0, 0, 0, 0);
            }
        }

        /// <inheritdoc />
        public static bool operator ==(BtAddress x, BtAddress y) => x.AddressBytes.SequenceEqual(y.AddressBytes);

        /// <inheritdoc />
        public static bool operator !=(BtAddress x, BtAddress y) => !x.AddressBytes.SequenceEqual(y.AddressBytes);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is BtAddress))
                return false;

            return (BtAddress)obj == this;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            int hash = this.AddressBytes[0];
            for (var i = 1; i < this.AddressBytes.Length; i++)
                hash ^= this.AddressBytes[i];

            return hash;
        }

        /// <summary>
        ///     Returns a string representing this Bluetooth device address, in MAC-48 format.
        /// </summary>
        /// <returns>A string representing this Bluetooth device address.</returns>
        public override string ToString()
        {
            return
                $"{this.AddressBytes[0]:X2}:{this.AddressBytes[1]:X2}:{this.AddressBytes[2]:X2}:{this.AddressBytes[3]:X2}:{this.AddressBytes[4]:X2}:{this.AddressBytes[5]:X2}";
        }
    }
}

namespace AjKeyvs.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BigBitSet
    {
        private BigArray<byte> bytes;

        public bool this[ulong index]
        {
            get
            {
                ulong position = index >> 8;
                ushort offset = (ushort)(index & 0x07);
                byte result = this.bytes[position];
                byte bit = (byte)(1 << offset);

                return (result & bit) != 0;
            }

            set
            {
                if (this.bytes == null)
                {
                    if (value == false)
                        return;
                    this.bytes = new BigArray<byte>();
                }

                ulong position = index >> 8;
                ushort offset = (ushort) (index & 0x07);
                byte result = this.bytes[position];
                byte bit = (byte) (1 << offset);
                if (value == false)
                    result &= (byte) (0xff - bit);
                else
                    result |= bit;

                this.bytes[position] = result;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AssaultCubeHack
{

    public class LocatableEntity
    {

        public struct Offsets
        {
            public int xPos, yPos, zPos;

            public Offsets(int xPos, int yPos, int zPos)
            {
                this.xPos = xPos;
                this.yPos = yPos;
                this.zPos = zPos;

            }
        }

        public Offsets offsets = new Offsets(
                0x04,
                0x08,
                0x0C
            );

        public int baseAddress;

        public float xPos, yPos, zPos;


        public Memory pm;

       public LocatableEntity(int baseAddress, int[] multiLevel, Memory pm)
        {
            this.baseAddress = pm.ReadMultiLevelPointer(baseAddress, 4, multiLevel);
            this.pm = pm;
        }

        public LocatableEntity(int baseAddress, Memory pm)
        {
            this.baseAddress = baseAddress;
            this.pm = pm;
        }

        public float getZPos()
        {
            return this.zPos;
        }

        public bool loadLocationData()
        {
            byte[] buffer = new byte[0x10];
            pm.ReadMem(baseAddress, 0x10, out buffer);

            this.xPos = BitConverter.ToSingle(buffer, this.offsets.xPos);
            this.yPos = BitConverter.ToSingle(buffer, this.offsets.yPos);
            this.zPos = BitConverter.ToSingle(buffer, this.offsets.zPos);

            if (this.xPos > 0)
            {
                return true;
            }

            return false;
        }

        public bool valid()
        {
            return this.xPos > 0;
        }
    }
}

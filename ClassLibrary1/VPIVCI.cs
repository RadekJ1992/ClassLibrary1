using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public class VPIVCI {
        public int VPI;
        public int VCI;

        public VPIVCI(int VPI, int VCI) {
            this.VPI = VPI;
            this.VCI = VCI;
        }
        public VPIVCI() {}
    }

    public class VPIVCIComparer : IEqualityComparer<VPIVCI> {

        public bool Equals(VPIVCI vpivci1, VPIVCI vpivci2) {
            bool temp = false;
            if (vpivci1.VCI == vpivci2.VCI && vpivci1.VPI == vpivci2.VPI) temp = true;
            return temp;
        }

        public int GetHashCode(VPIVCI vpivci) {
            return vpivci.VPI.GetHashCode() + vpivci.VCI.GetHashCode();
        }

    }
}

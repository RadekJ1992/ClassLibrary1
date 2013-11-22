using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public class PortVPIVCI {
        public int port;
        public int VPI;
        public int VCI;

        public PortVPIVCI(int port,int VPI, int VCI) {
            this.port = port;
            this.VPI = VPI;
            this.VCI = VCI;
        }
        public PortVPIVCI() {}
    }

    public class PortVPIVCIComparer : IEqualityComparer<PortVPIVCI> {

        public bool Equals(PortVPIVCI portvpivci1, PortVPIVCI portvpivci2) {
            bool temp = false;
            if (portvpivci1.port == portvpivci2.port && portvpivci1.VCI == portvpivci2.VCI && portvpivci1.VPI == portvpivci2.VPI) temp = true;
            return temp;
        }

        public int GetHashCode(PortVPIVCI portvpivci) {
            return portvpivci.port.GetHashCode() + portvpivci.VPI.GetHashCode() + portvpivci.VCI.GetHashCode();
        }

    }
}

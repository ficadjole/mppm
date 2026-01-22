using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PhaseImpedanceData : IdentifiedObject
    {

        private float b;
        private float r;
        private int sequenceNumber;
        private float x;
        private long phaseImpedance = 0;

        public PhaseImpedanceData(long globalId) : base(globalId)
        {
        }

        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public float R
        {
            get { return r; }
            set { r = value; }
        }

        public int SequenceNumber
        {
            get { return sequenceNumber; }
            set { sequenceNumber = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public long PhaseImpedance
        {
            get { return phaseImpedance; }
            set { phaseImpedance = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PhaseImpedanceData x = (PhaseImpedanceData)obj;
                return (x.b == this.b &&
                        x.r == this.r &&
                        x.sequenceNumber == this.sequenceNumber &&
                        x.x == this.x &&
                        x.phaseImpedance == this.phaseImpedance);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.PHASEIMPDATA_B:
                case ModelCode.PHASEIMPDATA_R:
                case ModelCode.PHASEIMPDATA_SEQNUM:
                case ModelCode.PHASEIMPDATA_X:
                case ModelCode.PHASEIMPDATA_PHASEIMP:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PHASEIMPDATA_B:
                    property.SetValue(b);
                    break;
                case ModelCode.PHASEIMPDATA_R:
                    property.SetValue(r);
                    break;
                case ModelCode.PHASEIMPDATA_SEQNUM:
                    property.SetValue(sequenceNumber);
                    break;
                case ModelCode.PHASEIMPDATA_X:
                    property.SetValue(x);
                    break;
                case ModelCode.PHASEIMPDATA_PHASEIMP:
                    property.SetValue(phaseImpedance);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PHASEIMPDATA_B:
                    b = property.AsFloat();
                    break;
                case ModelCode.PHASEIMPDATA_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.PHASEIMPDATA_SEQNUM:
                    sequenceNumber = property.AsInt();
                    break;
                case ModelCode.PHASEIMPDATA_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.PHASEIMPDATA_PHASEIMP:
                    phaseImpedance = property.AsLong();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (phaseImpedance != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.PHASEIMPDATA_PHASEIMP] = new List<long>();
                references[ModelCode.PHASEIMPDATA_PHASEIMP].Add(phaseImpedance);
            }
            base.GetReferences(references, refType);
        }
    }

    #endregion IReference implementation

}



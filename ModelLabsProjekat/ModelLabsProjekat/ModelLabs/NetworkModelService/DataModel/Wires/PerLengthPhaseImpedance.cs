using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthPhaseImpedance : PerLengthImpedance
    {

        private int conductorCount;
        private List<long> phaseImpedanceData = new List<long>();


        public PerLengthPhaseImpedance(long globalId) : base(globalId)
        {
        }

        public int ConductorCount
        {
            get { return conductorCount; }
            set { conductorCount = value; }
        }

        public List<long> PhaseImpedanceData
        {
            get { return phaseImpedanceData; }
            set { phaseImpedanceData = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PerLengthPhaseImpedance x = (PerLengthPhaseImpedance)obj;
                return (x.conductorCount == this.conductorCount &&
                        CompareHelper.CompareLists(x.phaseImpedanceData, this.phaseImpedanceData, true));
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
                case ModelCode.PERLENPHASEIMP_CONDCOUNT:
                case ModelCode.PERLENPHASEIMP_PHASEIMPDATA:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PERLENPHASEIMP_CONDCOUNT:
                    property.SetValue(conductorCount);
                    break;
                case ModelCode.PERLENPHASEIMP_PHASEIMPDATA:
                    property.SetValue(phaseImpedanceData);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }

        }

        public override void SetProperty(Property property)
        {
            switch(property.Id)
            {
                case ModelCode.PERLENPHASEIMP_CONDCOUNT:
                    conductorCount = property.AsInt();
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
                return phaseImpedanceData.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (phaseImpedanceData != null && phaseImpedanceData.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PERLENPHASEIMP_PHASEIMPDATA] = phaseImpedanceData.GetRange(0, phaseImpedanceData.Count);
            }
            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            if (referenceId == ModelCode.PHASEIMPDATA_PHASEIMP)
            {
                phaseImpedanceData.Add(globalId);
            }
            else
            {
                base.AddReference(referenceId, globalId);
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            if (referenceId == ModelCode.PHASEIMPDATA_PHASEIMP)
            {
                if (phaseImpedanceData.Contains(globalId))
                {
                    phaseImpedanceData.Remove(globalId);
                }
                else
                {
                    Console.WriteLine("WARNING: Trying to remove reference that does not exist!");
                }
            }
            else
            {
                base.RemoveReference(referenceId, globalId);
            }
        }


        #endregion IReference implementation
    }
}
